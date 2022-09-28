﻿

namespace TwoWindowToDo.Contracts
{
    public interface INavigationAware
    {
        void OnNavigatedTo(object parameter);
        void OnNavigatedFrom();
    }
}
