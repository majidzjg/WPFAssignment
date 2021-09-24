using Prism.Commands;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using WPFAssignment.Api;

namespace WPFAssignment.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private IList<Post> _postDetails;
        private List<List<int>> _ids;
        private List<List<int>> _userIds;

        public MainWindowViewModel()
        {
            GetButtonClicked = new DelegateCommand(GetPostDetailsAsync);
            SquareClicked = new DelegateCommand(ChangeDataDetails);
        }

        #region Properties  
        public bool IsLoadData { get; set; }
        public bool IsShowId { get; set; }
        public string ResponseMessage { get; set; } = "Welcome to WPF Assignment!";
        public List<List<int>> Data { get; set; }
        #endregion

        public DelegateCommand GetButtonClicked { get; set; }
        public DelegateCommand SquareClicked { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>  
        /// Fetches post details from API
        /// </summary>  
        private async void GetPostDetailsAsync()
        {
            _postDetails = await JsonPlaceHolderApiManager.GetPostListAsync();
            if (_postDetails.Any())
            {
                var groupedList = _postDetails.Select((x, i) => new { Index = i, Value = x })
                                              .GroupBy(x => x.Index / 10)
                                              .Select(x => x.Select(v => v.Value).ToList())
                                              .ToList();
                _ids = groupedList.Select(group => group.Select(v => v.Id).ToList()).ToList();
                _userIds = groupedList.Select(group => group.Select(v => v.UserId).ToList()).ToList();

                IsLoadData = true;
                IsShowId = true;
                Data = _ids;
                ResponseMessage = "Post details has successfully been fetched. Ids are listed below!";
            }
            else
            {
                ResponseMessage = "Failed to fetch posts details.";
            }

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(IsLoadData)));
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Data)));
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ResponseMessage)));
            }
        }

        /// <summary>  
        /// Update Data details
        /// </summary>  
        private void ChangeDataDetails()
        {
            IsShowId = !IsShowId;

            if (IsShowId)
            {
                Data = _ids;
                ResponseMessage = "Ids are listed below!";
            }
            else
            {
                Data = _userIds;
                ResponseMessage = "User Ids are listed below!";
            }

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Data)));
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ResponseMessage)));
            }
        }
    }
}
