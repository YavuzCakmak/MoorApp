using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moor.Core.Constant;
using Moor.Model.Utilities.TokenModel;
using Moor.Service.Exceptions;
using Moor.Service.Models.Dto.ResponseDto;
using Moor.Service.Utilities.AppSettings;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Moor.Service.Utilities.AuthorizeHelpers
{
    public class TokenHelper
    {
        private readonly IOptions<MoorSettings> _moorSettings;

        public TokenHelper(IOptions<MoorSettings> moorSettings)
        {
            _moorSettings = moorSettings;
        }

        public string CreateToken(TokenModel tokenModel)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_moorSettings.Value.JwtSetting.Key));

            var credentials = new SigningCredentials(key: securityKey, algorithm: SecurityAlgorithms.HmacSha256Signature);

            tokenModel.RefreshTokenEndDate = DateTime.Now.AddHours(8).ToString();
            tokenModel.ValidTo = DateTime.Now.AddHours(1).ToString();
            tokenModel.RefreshToken = HashingHelper.CreateRefrehToken(tokenModel.Username);

            var jwtToken = new JwtSecurityToken(
                issuer: _moorSettings.Value.JwtSetting.Issuer,
                audience: _moorSettings.Value.JwtSetting.Audience,
                claims: GetClaims(tokenModel),
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public TokenModel ValidateToken(string token)
        {
            try
            {
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
                IJsonSerializer serializer = new JsonNetSerializer();
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();

                UtcDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);

                var personnel = decoder.DecodeToObject<TokenModel>(token, _moorSettings.Value.JwtSetting.Key, verify: true);

                if (personnel == null || DateTime.Parse(!string.IsNullOrEmpty(personnel.ValidTo) ? personnel.ValidTo : personnel.RefreshTokenEndDate) < DateTime.Now)
                {
                    throw new ClientSideException(TokenConstant.EXPIRED_TOKEN);
                }

                return personnel;
            }
            catch (Exception ex)
            {
                throw new ClientSideException(TokenConstant.INVALID_TOKEN);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string? token)
        {
            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _moorSettings.Value.JwtSetting.Issuer,
                    ValidAudience = _moorSettings.Value.JwtSetting.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_moorSettings.Value.JwtSetting.Key)),
                    ClockSkew = TimeSpan.FromMinutes(2)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

                if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCultureIgnoreCase))
                    throw new ClientSideException(TokenConstant.INVALID_TOKEN);

                return principal;
            }
            catch (Exception)
            {
                throw new ClientSideException(TokenConstant.INVALID_TOKEN);
            }
        }

        public Claim[] GetClaims(TokenModel personnelToken)
        {
            var claims = new List<Claim>
            {
                new Claim("PersonnelId", personnelToken.PersonnelId.ToString()),
                new Claim("UserName", personnelToken.Username),
                new Claim("RefreshToken", personnelToken.RefreshToken),
                new Claim("RefreshTokenEndDate", personnelToken.RefreshTokenEndDate),
                new Claim("AgencyId", personnelToken.AgencyId.ToString()),
                new Claim("AgencyMedia", !string.IsNullOrEmpty(personnelToken.AgencyMedia) ?personnelToken.AgencyMedia.ToString()  : string.Empty),
                new Claim("DriverMedia", !string.IsNullOrEmpty(personnelToken.DriverMedia) ?personnelToken.DriverMedia.ToString()  : string.Empty),
                new Claim("DriverId", personnelToken.DriverId.ToString()),
                new Claim("ValidTo", personnelToken.ValidTo),
                new Claim("Roles", JsonConvert.SerializeObject(personnelToken.Roles), JsonClaimValueTypes.JsonArray)
            };

            return claims.ToArray();
        }

        public string CreateTokenForExtend(IEnumerable<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_moorSettings.Value.JwtSetting.Key));

            var credentials = new SigningCredentials(key: securityKey, algorithm: SecurityAlgorithms.HmacSha256Signature);

            var jwtToken = new JwtSecurityToken(
                issuer: _moorSettings.Value.JwtSetting.Issuer,
                audience: _moorSettings.Value.JwtSetting.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
