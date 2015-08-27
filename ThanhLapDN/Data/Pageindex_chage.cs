using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThanhLapDN.Data
{
    public class Pageindex_chage
    {
        public string result(int tongsotin, int sotin, string cat_seo_url, int idarea, int _page, int type, string _year,string _field, string _search, string _status)
        {
            string _re = string.Empty;
            int kiemtradu = tongsotin % sotin;
            int _sotrang;
            int _trangCuoi;
            if (_page == 0)
            {
                _page = 1;
            }
            if (kiemtradu != 0)
            {
                _sotrang = (tongsotin / sotin) + 1;
                _trangCuoi = (tongsotin / sotin) + 1;
            }
            else
            {
                _sotrang = (tongsotin / sotin);
                _trangCuoi = (tongsotin / sotin);
            }
            if (_sotrang == 1)
            {
                _re = "";
            }
            else
            {
                int s = 1;
                if (_sotrang > 10)
                {
                    if (_page >= 10 && _page < _sotrang)
                    {
                        _sotrang = _page + 1;
                        s = _page - 10 + 2;
                    }
                    else if (_page == _sotrang)
                    {
                        _sotrang = _page;
                        s = _page - 10 + 1;
                    }
                    else _sotrang = 10;
                }
                if (type == 1)
                {
                    _re += "<a href='/Pages/" + cat_seo_url + ".aspx?year=" + _year + "&field=" + _field + "&status=" + _status + "&search=" + _search + "'>Trước</a>";
                }
                for (int i = s; i <= _sotrang; i++)
                {
                    if (_page == i)
                    {
                        _re += "<b>" + i + "</b>";
                    }
                    else
                    {
                        if (type == 1)
                        {
                            if (i == _sotrang && _page >= 10)
                            {
                                _re += "<a href='/Pages/" + cat_seo_url + ".aspx?page=" + (_page + 1) + "&year=" + _year + "&field=" + _field + "&status=" + _status + "&search=" + _search + "'> >> </a>";
                            }
                            else if (i == s && _page >= 10)
                            {
                                _re += "<a href='/Pages/" + cat_seo_url + ".aspx?page=" + (_page - 1) + "&year=" + _year + "&field=" + _field + "&status=" + _status + "&search=" + _search + "'> << </a>";
                            }
                            else
                                _re += "<a href='/Pages/" + cat_seo_url + ".aspx?page=" + i + "&year=" + _year + "&field=" + _field + "&status=" + _status + "&search=" + _search + "'>" + i + "</a>";
                        }
                    }
                }
                _re += "<a href='/Pages/" + cat_seo_url + ".aspx?page=" + _trangCuoi + "&year=" + _year + "&field=" + _field + "&status=" + _status + "&search=" + _search + "'>Sau</a>";
            }
            return _re;
        }
        public string result_cks(int tongsotin, int sotin, string cat_seo_url, int idarea, int _page, int type,string _month, string _year, string _field, string _search)
        {
            string _re = string.Empty;
            int kiemtradu = tongsotin % sotin;
            int _sotrang;
            int _trangCuoi;
            if (_page == 0)
            {
                _page = 1;
            }
            if (kiemtradu != 0)
            {
                _sotrang = (tongsotin / sotin) + 1;
                _trangCuoi = (tongsotin / sotin) + 1;
            }
            else
            {
                _sotrang = (tongsotin / sotin);
                _trangCuoi = (tongsotin / sotin);
            }
            if (_sotrang == 1)
            {
                _re = "";
            }
            else
            {
                int s = 1;
                if (_sotrang > 10)
                {
                    if (_page >= 10 && _page < _sotrang)
                    {
                        _sotrang = _page + 1;
                        s = _page - 10 + 2;
                    }
                    else if (_page == _sotrang)
                    {
                        _sotrang = _page;
                        s = _page - 10 + 1;
                    }
                    else _sotrang = 10;
                }
                if (type == 1)
                {
                    _re += "<a href='/Pages/" + cat_seo_url + ".aspx?month=" + _month + "&year=" + _year + "&field=" + _field + "&search=" + _search + "'>Trước</a>";
                }
                for (int i = s; i <= _sotrang; i++)
                {
                    if (_page == i)
                    {
                        _re += "<b>" + i + "</b>";
                    }
                    else
                    {
                        if (type == 1)
                        {
                            if (i == _sotrang && _page >= 10)
                            {
                                _re += "<a href='/Pages/" + cat_seo_url + ".aspx?page=" + (_page + 1) + "&month=" + _month + "&year=" + _year + "&field=" + _field + "&search=" + _search + "'> >> </a>";
                            }
                            else if (i == s && _page >= 10)
                            {
                                _re += "<a href='/Pages/" + cat_seo_url + ".aspx?page=" + (_page - 1) + "&month=" + _month + "&year=" + _year + "&field=" + _field + "&search=" + _search + "'> << </a>";
                            }
                            else
                                _re += "<a href='/Pages/" + cat_seo_url + ".aspx?page=" + i + "&month=" + _month + "&year=" + _year + "&field=" + _field + "&search=" + _search + "'>" + i + "</a>";
                        }
                    }
                }
                _re += "<a href='/Pages/" + cat_seo_url + ".aspx?page=" + _trangCuoi + "&month=" + _month + "&year=" + _year + "&field=" + _field + "&search=" + _search + "'>Sau</a>";
            }
            return _re;
        }
        public string result_web(int tongsotin, int sotin, string cat_seo_url, int idarea, int _page, int type, string _month, string _year, string _field, string _search, int _tinhtrang, int _congno)
        {
            string _re = string.Empty;
            int kiemtradu = tongsotin % sotin;
            int _sotrang;
            int _trangCuoi;
            if (_page == 0)
            {
                _page = 1;
            }
            if (kiemtradu != 0)
            {
                _sotrang = (tongsotin / sotin) + 1;
                _trangCuoi = (tongsotin / sotin) + 1;
            }
            else
            {
                _sotrang = (tongsotin / sotin);
                _trangCuoi = (tongsotin / sotin);
            }
            if (_sotrang == 1)
            {
                _re = "";
            }
            else
            {
                int s = 1;
                if (_sotrang > 10)
                {
                    if (_page >= 10 && _page < _sotrang)
                    {
                        _sotrang = _page + 1;
                        s = _page - 10 + 2;
                    }
                    else if (_page == _sotrang)
                    {
                        _sotrang = _page;
                        s = _page - 10 + 1;
                    }
                    else _sotrang = 10;
                }
                if (type == 1)
                {
                    _re += "<a href='/Pages/" + cat_seo_url + ".aspx?month=" + _month + "&year=" + _year + "&field=" + _field + "&search=" + _search  + "&congno=" + _congno + "&tinhtrang=" + _tinhtrang + "'>Trước</a>";
                }
                for (int i = s; i <= _sotrang; i++)
                {
                    if (_page == i)
                    {
                        _re += "<b>" + i + "</b>";
                    }
                    else
                    {
                        if (type == 1)
                        {
                            if (i == _sotrang && _page >= 10)
                            {
                                _re += "<a href='/Pages/" + cat_seo_url + ".aspx?page=" + (_page + 1) + "&month=" + _month + "&year=" + _year + "&field=" + _field + "&search=" + _search + "&congno=" + _congno + "&tinhtrang=" + _tinhtrang + "'> >> </a>";
                            }
                            else if (i == s && _page >= 10)
                            {
                                _re += "<a href='/Pages/" + cat_seo_url + ".aspx?page=" + (_page - 1) + "&month=" + _month + "&year=" + _year + "&field=" + _field + "&search=" + _search + "&congno=" + _congno + "&tinhtrang=" + _tinhtrang + "'> << </a>";
                            }
                            else
                                _re += "<a href='/Pages/" + cat_seo_url + ".aspx?page=" + i + "&month=" + _month + "&year=" + _year + "&field=" + _field + "&search=" + _search + "&congno=" + _congno + "&tinhtrang=" + _tinhtrang + "'>" + i + "</a>";
                        }
                    }
                }
                _re += "<a href='/Pages/" + cat_seo_url + ".aspx?page=" + _trangCuoi + "&month=" + _month + "&year=" + _year + "&field=" + _field + "&search=" + _search + "&congno=" + _congno + "&tinhtrang=" + _tinhtrang + "'>Sau</a>";
            }
            return _re;
        }
    }
}