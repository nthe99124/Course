﻿using AutoMapper;
using CourseProject.API.Common.Cache;
using CourseProject.API.Common.Constant;
using CourseProject.API.Common.Repository;
using CourseProject.API.Services.Base;
using CourseProject.Model.BaseEntity;
using System.Text.Json;

namespace CourseProject.API.Services
{
    public interface ILogEntryService
    {
        void LogInformation(string message, string requestApi = "");
        void LogError(string message, string requestApi = "");
    }
    public class LogEntryService : BaseService, ILogEntryService
    {
        private readonly IConfiguration _config;
        public LogEntryService(IHttpContextAccessor httpContextAccessor,
                                IDistributedCacheCustom cache,
                                IUnitOfWork unitOfWork, IMapper mapper,
                                IConfiguration config) : base(httpContextAccessor, cache, unitOfWork, mapper)
        {
            _config = config;
        }

        /// <summary>
        /// Thực hiện insert log gọi bđb nhưng không cần hứng
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <param name="message"></param>
        /// <param name="requestApi"></param>
        public void LogInformation(string message, string requestApi = "")
        {
            InsertLogLevel(message, requestApi, LogLevelConstant.Information);
        }

        /// <summary>
        /// Thực hiện insert log gọi bđb nhưng không cần hứng
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <param name="message"></param>
        /// <param name="requestApi"></param>
        public void LogError(string message, string requestApi = "")
        {
            InsertLogLevel(message, requestApi, LogLevelConstant.Error);
        }

        #region Private Method
        public void InsertLogLevel(string message, string requestApi, string logLevel)
        {
            var listLogLevel = _cache.GetValueCache<List<string>>(CacheKeyConstant.LogLevel);
            if (listLogLevel == null)
            {
                var cacheConfig = _config.GetSection(CacheKeyConstant.LogLevel).ToString();
                if (cacheConfig != null)
                {
                    listLogLevel = JsonSerializer.Deserialize<List<string>>(cacheConfig);
                    _cache.SetString(CacheKeyConstant.LogLevel, JsonSerializer.Serialize(cacheConfig));
                }
            }

            if (listLogLevel != null && listLogLevel.Count > 0 && listLogLevel.Contains(logLevel))
            {
                var logEntry = new LogEntry
                {
                    LogLevel = logLevel,
                    Message = message,
                    RequestApi = requestApi

                };
                _unitOfWork.LogEntryRepository.Create(logEntry);
                _unitOfWork.Commit();
            }
        }
        #endregion
    }
}
