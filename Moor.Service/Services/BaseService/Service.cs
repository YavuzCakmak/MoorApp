using Microsoft.EntityFrameworkCore;
using Moor.Core.Entities.Base;
using Moor.Core.Extension.String;
using Moor.Core.Repositories;
using Moor.Core.Services.BaseService;
using Moor.Core.UnitOfWorks;
using Moor.Core.Utilities.DataFilter;
using Moor.Service.Exceptions;
using System.Linq.Expressions;
using System.Reflection;

namespace Moor.Service.Services.BaseService
{
    public class Service<T> : IService<T> where T : CoreEntity
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public Service(IGenericRepository<T> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            return entities;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(DataFilterModel dataFilterModel)
        {
            FilterConvertCaseInsensitive(dataFilterModel);
            return await _repository.GetAll(dataFilterModel).ToListAsync();
        }

        private void FilterConvertCaseInsensitive(DataFilterModel dataFilterModel)
        {
            if (dataFilterModel.IsNotNull() && dataFilterModel.Filters.IsNotNullOrEmpty())
            {
                dataFilterModel.Filters = dataFilterModel.Filters.Replace("@=", "@=*").Replace("@=**", "@=*");

                foreach (string filter in dataFilterModel.Filters.Split(','))
                {
                    PropertyInfo currentProp = null;

                    if (filter.Contains("=="))
                    {

                        string _filter = filter.Split("==")[0];

                        #region Sub

                        if (_filter.Contains("."))
                        {
                            string SubEntityVal = _filter.Split('.')[0];
                            string SubPropVal = _filter.Split('.')[1];

                            if (SubEntityVal.IsNullOrEmpty() || SubPropVal.IsNullOrEmpty()) return;

                            currentProp = typeof(T).GetProperties().Where(x => SubEntityVal.ToLower() == x.Name.ToLower(culture: System.Globalization.CultureInfo.InvariantCulture)).FirstOrDefault();

                            if (currentProp.IsNotNull())
                            {
                                var subProp = currentProp.GetType().GetProperty(SubPropVal);

                                if (subProp.IsNotNull() && subProp.PropertyType == typeof(string) && !filter.Contains("==*"))
                                {
                                    dataFilterModel.Filters = dataFilterModel.Filters.Replace(filter, filter.Replace("==", "==*"));
                                }

                                string _value = filter.Split("==")[1];
                                if (currentProp.IsNotNull() &&
                                   (currentProp.PropertyType == typeof(DateTime) || currentProp.PropertyType == typeof(DateTime?)) &&
                                   _value.IsDateTime() && !_value.Contains("T"))
                                {
                                    string replaceStr = _filter + ">=" + _value + "T00:00:00" + "," + _filter + "<=" + _value + "T23:59:59";

                                    dataFilterModel.Filters = dataFilterModel.Filters.Replace(filter, replaceStr);
                                }
                            }
                        }

                        #endregion

                        else
                        {
                            currentProp = typeof(T).GetProperties().Where(x => _filter.ToLower() == x.Name.ToLower()).FirstOrDefault();

                            if (currentProp.IsNotNull() && currentProp.PropertyType == typeof(string) && !filter.Contains("==*"))
                            {
                                dataFilterModel.Filters = dataFilterModel.Filters.Replace(filter, filter.Replace("==", "==*"));
                            }

                            string _value = filter.Split("==")[1];
                            if (currentProp.IsNotNull() &&
                               (currentProp.PropertyType == typeof(DateTime) || currentProp.PropertyType == typeof(DateTime?)) &&
                               _value.IsDateTime() && !_value.Contains("T"))
                            {
                                string replaceStr = _filter + ">=" + _value + "T00:00:00" + "," + _filter + "<=" + _value + "T23:59:59";

                                dataFilterModel.Filters = dataFilterModel.Filters.Replace(filter, replaceStr);
                            }
                        }
                    }
                    else if (filter.Contains("@=*"))
                    {
                        string _filter = filter.Split("@=*")[0];

                        #region Sub

                        if (_filter.Contains("."))
                        {
                            string SubEntityVal = _filter.Split('.')[0];
                            string SubPropVal = _filter.Split('.')[1];

                            if (SubEntityVal.IsNullOrEmpty() || SubPropVal.IsNullOrEmpty()) return;

                            currentProp = typeof(T).GetProperties().Where(x => SubEntityVal.ToLower() == x.Name.ToLower(culture: System.Globalization.CultureInfo.InvariantCulture)).FirstOrDefault();

                            if (currentProp.IsNotNull())
                            {
                                var subProp = currentProp.GetType().GetProperty(SubPropVal);

                                if (subProp.IsNotNull() && subProp.PropertyType != typeof(string))
                                {
                                    dataFilterModel.Filters = dataFilterModel.Filters.Replace(filter, filter.Replace("@=*", "=="));
                                }
                            }
                        }

                        #endregion

                        else
                        {
                            currentProp = typeof(T).GetProperties().Where(x => _filter.ToLower() == x.Name.ToLower()).FirstOrDefault();

                            if (currentProp.IsNotNull() && currentProp.PropertyType != typeof(string))
                            {
                                dataFilterModel.Filters = dataFilterModel.Filters.Replace(filter, filter.Replace("@=*", "=="));
                            }
                        }
                    }
                    else if (filter.Contains(">=") || filter.Contains("<="))
                    {
                        string _filter = filter.Contains(">=") ? filter.Split(">=")[0] : filter.Split("<=")[0];
                        string _value = filter.Contains(">=") ? filter.Split(">=")[1] : filter.Split("<=")[1];

                        #region Sub

                        if (_filter.Contains("."))
                        {
                            string SubEntityVal = _filter.Split('.')[0];
                            string SubPropVal = _filter.Split('.')[1];

                            if (SubEntityVal.IsNullOrEmpty() || SubPropVal.IsNullOrEmpty()) return;

                            currentProp = typeof(T).GetProperties().Where(x => SubEntityVal.ToLower() == x.Name.ToLower(culture: System.Globalization.CultureInfo.InvariantCulture)).FirstOrDefault();

                            if (currentProp.IsNotNull())
                            {
                                var subProp = currentProp.GetType().GetProperty(SubPropVal);

                                if (subProp.IsNotNull() &&
                                    (subProp.PropertyType == typeof(DateTime) || subProp.PropertyType == typeof(DateTime?)) &&
                                    _value.IsDateTime() && !_value.Contains("T"))
                                {
                                    string replaceStr = _value;
                                    if (filter.Contains(">=") || filter.Contains(">"))
                                    {
                                        replaceStr += "T00:00:00";
                                    }
                                    else if (filter.Contains("<=") || filter.Contains("<"))
                                    {
                                        replaceStr += "T23:59:59";
                                    }

                                    dataFilterModel.Filters = dataFilterModel.Filters.Replace(filter, filter.Replace(_value, replaceStr));
                                }
                            }
                        }

                        #endregion

                        else
                        {
                            currentProp = typeof(T).GetProperties().Where(x => _filter.ToLower() == x.Name.ToLower()).FirstOrDefault();

                            if (currentProp.IsNotNull() &&
                                (currentProp.PropertyType == typeof(DateTime) || currentProp.PropertyType == typeof(DateTime?)) &&
                                _value.IsDateTime() && !_value.Contains("T"))
                            {
                                string replaceStr = _value;
                                if (filter.Contains(">=") || filter.Contains(">"))
                                {
                                    replaceStr += "T00:00:00";
                                }
                                else if (filter.Contains("<=") || filter.Contains("<"))
                                {
                                    replaceStr += "T23:59:59";
                                }

                                dataFilterModel.Filters = dataFilterModel.Filters.Replace(filter, filter.Replace(_value, replaceStr));
                            }
                        }
                    }
                    else if (filter.Contains(">") || filter.Contains("<"))
                    {
                        string _filter = filter.Contains(">") ? filter.Split(">")[0] : filter.Split("<")[0];
                        string _value = filter.Contains(">") ? filter.Split(">")[1] : filter.Split("<")[1];

                        #region Sub

                        if (_filter.Contains("."))
                        {
                            string SubEntityVal = _filter.Split('.')[0];
                            string SubPropVal = _filter.Split('.')[1];

                            if (SubEntityVal.IsNullOrEmpty() || SubPropVal.IsNullOrEmpty()) return;

                            currentProp = typeof(T).GetProperties().Where(x => SubEntityVal.ToLower() == x.Name.ToLower(culture: System.Globalization.CultureInfo.InvariantCulture)).FirstOrDefault();

                            if (currentProp.IsNotNull())
                            {
                                var subProp = currentProp.GetType().GetProperty(SubPropVal);

                                if (subProp.IsNotNull() &&
                                    (subProp.PropertyType == typeof(DateTime) || subProp.PropertyType == typeof(DateTime?)) &&
                                    _value.IsDateTime() && !_value.Contains("T"))
                                {
                                    string replaceStr = _value;
                                    if (filter.Contains(">=") || filter.Contains(">"))
                                    {
                                        replaceStr += "T00:00:00";
                                    }
                                    else if (filter.Contains("<=") || filter.Contains("<"))
                                    {
                                        replaceStr += "T23:59:59";
                                    }

                                    dataFilterModel.Filters = dataFilterModel.Filters.Replace(filter, filter.Replace(_value, replaceStr));
                                }
                            }
                        }

                        #endregion

                        else
                        {
                            currentProp = typeof(T).GetProperties().Where(x => _filter.ToLower() == x.Name.ToLower()).FirstOrDefault();

                            if (currentProp.IsNotNull() &&
                                (currentProp.PropertyType == typeof(DateTime) || currentProp.PropertyType == typeof(DateTime?)) &&
                                _value.IsDateTime() && !_value.Contains("T"))
                            {
                                string replaceStr = _value;
                                if (filter.Contains(">=") || filter.Contains(">"))
                                {
                                    replaceStr += "T00:00:00";
                                }
                                else if (filter.Contains("<=") || filter.Contains("<"))
                                {
                                    replaceStr += "T23:59:59";
                                }

                                dataFilterModel.Filters = dataFilterModel.Filters.Replace(filter, filter.Replace(_value, replaceStr));
                            }
                        }
                    }
                }
            }
        }
        public async Task<T> GetByIdAsync(long id)
        {
            var value = await _repository.GetByIdAsync(id);
            if (value == null)
            {
                throw new ClientSideException($"{typeof(T).Name} not found");
            }
            return value;
        }

        public async Task RemoveAsync(T entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _repository.Where(expression);
        }
    }
}
