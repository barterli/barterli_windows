using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Windows.UI.Xaml.Data;
using Windows.Foundation;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Core;

namespace Barter.li.Win.Util
{
    public abstract class IncrementalLoadingCollection<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {
        private bool _IsBusy;

        #region ISupportIncrementalLoading

        public bool HasMoreItems
        {
            get { return CanLoadMoreItems(); }
        }       

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            if (_IsBusy)
            {
                throw new InvalidOperationException("Only one operation in flight at a time");
            }
            else
            {
                _IsBusy = true;
                return AsyncInfo.Run((c) => LoadMoreItemsAsync((int)count));
            }

        }

        #endregion

        #region private Methods

        async Task<LoadMoreItemsResult> LoadMoreItemsAsync(int count)
        {

            try
            {
                var items = await LoadNextItemsAsync(count);
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        this.Add(item);
                    };
                }
                return new LoadMoreItemsResult { Count = (uint)items.Count };
            }
            finally
            {
                _IsBusy = false;
            }
        }

        #endregion

        # region Overridable Methods

        protected abstract Task<ObservableCollection<T>> LoadNextItemsAsync(int count);
        protected abstract bool CanLoadMoreItems();        

        #endregion
    }
}
