namespace iH.UI.ViewModels
{
    using System.Linq;
    using System.Collections.Generic;

    using iH.Domain.Security.Entities;

    public class MenuViewModel
    {
        public Menu Item { get; set; }
        public bool HasChildren { get; set; }
        public List<Menu> Children { get; set; }

        public static List<MenuViewModel> GetViewModel(IList<Menu> menus)
        {
            List<MenuViewModel> items = new List<MenuViewModel>();

            menus = menus.OrderBy(o => o.DisplayOrder).ToList();

            foreach (var menu in menus)
            {
                if (menu.ParentId == null)
                {
                    MenuViewModel viewModel = new MenuViewModel();

                    viewModel.Item = menu;
                    var subMenus = menus.Where(m => m.ParentId == menu.MenuId);

                    if (subMenus.Count() > 0)
                    {
                        viewModel.HasChildren = true;
                        viewModel.Children = subMenus.OrderBy(o => o.DisplayOrder).ToList();
                    }

                    items.Add(viewModel);
                }
            }

            return items;
        }
    }
}