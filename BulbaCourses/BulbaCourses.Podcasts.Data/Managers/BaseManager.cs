﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
[assembly: InternalsVisibleTo("BulbaCourses.Podcasts.Web")]
namespace BulbaCourses.Podcasts.Data
{
    public abstract class BaseManager : IDisposable
    {
        protected readonly PodcastsContext dbContext;
        private bool _isDisposed = false;

        public BaseManager(PodcastsContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Dispose()
        {
            this.Dispose(true);

        }

        protected virtual void Dispose(bool flag)
        {
            if (_isDisposed) return;

            dbContext?.Dispose();
            _isDisposed = true;
            if (flag) GC.SuppressFinalize(this);
        }

        ~BaseManager()
        {
            this.Dispose(false);
        }
    }
}
