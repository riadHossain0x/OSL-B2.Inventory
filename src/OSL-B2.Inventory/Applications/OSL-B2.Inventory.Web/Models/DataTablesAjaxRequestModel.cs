using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSL_B2.Inventory.Web.Models
{
    public class DataTablesAjaxRequestModel
    {
        private readonly HttpRequestBase _request;

        public DataTablesAjaxRequestModel(HttpRequestBase request)
        {
            _request = request;
        }

        public int Start
        {
            get
            {
                return int.Parse(_request["start"]);
            }
        }

        public int Length
        {
            get
            {
                return int.Parse(_request["length"]);
            }
        }

        public string SearchText
        {
            get
            {
                return _request["search[value]"];
            }
        }

        public string SortColumn
        {
            get
            {
                return _request["columns[" + _request["order[0][column]"] + "][name]"];
            }
        }

        public int SortColumnIndex
        {
            get
            {
                return int.Parse(_request.QueryString["iSortCol_0"]);
            }
        }

        public string SortDirection
        {
            get
            {
                return _request["order[0][dir]"];
            }
        }
    }
}