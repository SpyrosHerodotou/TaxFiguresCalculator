﻿using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxFiguresCalculator.MVC.Interfaces;
using TaxFiguresCalculator.MVC.ViewModels;

namespace TaxFiguresCalculator.MVC.Services
{
    public class CachedDataManagerService : IDataManagerViewService
    {
        private readonly IMemoryCache _cache;
        private readonly DataManagerViewService _dataManagerViewService;
        private static readonly string _brandsKey = "brands";
        private static readonly string _typesKey = "types";
        private static readonly string _itemsKeyTemplate = "items-{0}-{1}-{2}-{3}";
        private static readonly TimeSpan _defaultCacheDuration = TimeSpan.FromSeconds(30);

        public CachedDataManagerService(IMemoryCache cache,
            DataManagerViewService dataManagerViewservice)
        {
            _cache = cache;
            _dataManagerViewService = dataManagerViewservice;
        }

        /// <summary>
        /// Utilisation of IMemoryCache for Retrieval of large Transaction Data
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="itemsPage"></param>
        /// <returns></returns>
        public async Task<TransactionsDataViewModel> GetTransactionsByCustomerAsync(int CustomerId, int pageIndex, int itemsPage)
        {
            string cacheKey = String.Format(_itemsKeyTemplate, pageIndex, itemsPage);
            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;
                return await _dataManagerViewService.GetTransactionsByCustomerAsync(CustomerId, pageIndex, itemsPage);
            });
        }
        /// <summary>
        /// Utilisation of IMemoryCache for Transcation View Model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TransactionViewModel GetTransactionViewModel(int id)
        {
            string cacheKey = String.Format(_itemsKeyTemplate, id);
            return  _cache.GetOrCreate(cacheKey, entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;
                return  _dataManagerViewService.GetTransactionViewModel(id);
            });
        }
    }
}
