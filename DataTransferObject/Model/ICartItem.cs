using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObject.Model
{
    public interface ICartItem
    {
        string Name { get; set; }
        int Id { get; set; }
    }
}
