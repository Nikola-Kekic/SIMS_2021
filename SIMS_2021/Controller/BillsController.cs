using SIMS_2021.DTOs;
using SIMS_2021.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_2021.Controller
{
    class BillsController
    {
        private BillService _billService = new BillService();

        public bool Buy(List<CartDTO> cart, long userId)
        {
            return _billService.Buy(cart, userId);
        }
        public List<BillDTO> GetAllForUser(long userId)
        {
            return _billService.GetAllForUser(userId);
        }
    }
}
