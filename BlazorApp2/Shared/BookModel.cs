﻿using LazurdIT.FluentOrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp2.Shared
{

    [FluentTable("Books")]
    public class BookModel : IFluentModel
    {
        [FluentField(name: "BookId", autoGenerated: true, isPrimary: true, allowNull: false)]
        public int BookId { get; set; }

        [FluentField(name: "Title", allowNull: false)]
        public string Title { get; set; } = string.Empty;
        [FluentField(name: "Author", allowNull: false)]
        public string Author { get; set; } = string.Empty;

        [FluentField(name: "PublishedYear", allowNull: true)]
        public int? PublishedYear { get; set; }
        [FluentField(name: "Quantity", allowNull: true)]
        public int? Quantity { get; set; }

    }
}