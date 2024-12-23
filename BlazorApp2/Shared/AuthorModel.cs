﻿using LazurdIT.FluentOrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp2.Shared
{
    [FluentTable("Authors")]
    public class AuthorModel : IFluentModel
    {
        [FluentField(name: "AuthorId", autoGenerated: true, isPrimary: true, allowNull: false)]
        public int AuthorId { get; set; }

        [FluentField(name: "Name", allowNull: false)]
        public string Name { get; set; } = string.Empty;
        public ICollection<BookModel> Books { get; set; } = new List<BookModel>();
    }
}
