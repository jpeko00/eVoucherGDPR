using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using eVoucherGDPR.Model;
using eVoucherGDPR.Data;

namespace eVoucherGDPR.ViewModel
{
    class ManualItemsViewModel
    {
        private ObservableCollection<ManualItem> items;
        public ObservableCollection<ManualItem> Items
        {
            get { return items; }
            set
            {
                items = value;
            }
        }

        public ManualItemsViewModel()
        {

            Items = new ObservableCollection<ManualItem>();

            ManualListData _context = new ManualListData();

            foreach (var mlitem in _context.items)
            {
                Items.Add(mlitem);
            }
        }

    }
}
