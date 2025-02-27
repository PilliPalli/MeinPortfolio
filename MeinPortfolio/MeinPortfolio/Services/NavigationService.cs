using System;
using System.Collections.Generic;

namespace MeinPortfolio.Services
{
    public enum NavigationSection
    {
        Home,
        About,
        Projects,
        Contact,
        Skills
    }

    public class NavigationService
    {
        private NavigationSection _currentSection = NavigationSection.Home;
        private readonly Stack<NavigationSection> _navigationHistory = new();

        public event Action<NavigationSection> OnNavigate;

        public NavigationSection CurrentSection => _currentSection;

        public void NavigateTo(NavigationSection section)
        {
            if (_currentSection != section)
            {
                _navigationHistory.Push(_currentSection);
                _currentSection = section;
                OnNavigate?.Invoke(section);
            }
        }

        public bool CanGoBack => _navigationHistory.Count > 0;

        public void GoBack()
        {
            if (CanGoBack)
            {
                _currentSection = _navigationHistory.Pop();
                OnNavigate?.Invoke(_currentSection);
            }
        }

        public void GoHome()
        {
            if (_currentSection != NavigationSection.Home)
            {
                _navigationHistory.Push(_currentSection);
                _currentSection = NavigationSection.Home;
                OnNavigate?.Invoke(_currentSection);
            }
        }

        public string GetSectionPath()
        {
            return _currentSection switch
            {
                NavigationSection.Home => "~",
                NavigationSection.About => "~/about",
                NavigationSection.Projects => "~/projects",
                NavigationSection.Contact => "~/contact",
                NavigationSection.Skills => "~/skills",
                _ => "~"
            };
        }
    }
}
