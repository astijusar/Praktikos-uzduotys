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
        private IMenuManager menuManager;

        public NavigationManager(IMenuManager menuManager)
        {
            this.menuManager = menuManager;
        }

        public void StartNavigation()
        {
            menuManager.MainMenu();
        }
    }
}
