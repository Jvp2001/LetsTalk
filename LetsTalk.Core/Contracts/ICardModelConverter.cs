using System;
using System.Collections.Generic;
using System.Text;
using LetsTalk.Core.Models;

namespace LetsTalk.Core.Contracts;

public interface ICardModelConverter
{
    public CardsTableRowModel ToCardModel();
}
