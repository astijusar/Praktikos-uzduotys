using Part1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1
{
    public class NavigationManager : INavigationManager
    {
        private IMenuManager _menuManager;

        public NavigationManager(IMenuManager menuManager)
        {
            _menuManager = menuManager;
        }

        public void StartNavigation()
        {
            _menuManager.MainMenu();
        }
    }
}
