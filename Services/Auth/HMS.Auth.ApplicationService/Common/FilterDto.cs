using Microsoft.AspNetCore.Mvc;

namespace HMS.Auth.ApplicationService.Common
{
    public class FilterDto
    {
        [FromQuery(Name = "PageIndex")]
        public int PageIndex { get; set; } = 1;

        [FromQuery(Name = "PageSize")]
        public int PageSize { get; set; } = 5;

        private string _keyWord;
        [FromQuery(Name = "KeyWord")]
        public string? KeyWord
        {
            get => _keyWord;
            set => _keyWord = value?.Trim();
        }

        public int Skip()
        {
            return (PageIndex - 1) * PageSize;
        }
    }
}
