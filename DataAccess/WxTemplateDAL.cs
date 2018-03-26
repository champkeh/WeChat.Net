using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class WxTemplateDAL
    {
        static testEntities db = new testEntities();


        public static wx_templates Get( string appid, int id )
        {
            return db.wx_templates.SingleOrDefault( qu => qu.appid == appid && qu.id == id );
        }

        public static wx_templates Get( string appid, string tmplId )
        {
            return db.wx_templates.SingleOrDefault( qu => qu.appid == appid && qu.template_id == tmplId );
        }


        public static List<wx_templates> List( string appid )
        {
            return db.wx_templates.Where( qu => qu.appid == appid ).ToList( );
        }


        public static void Remove( string appid, string tmplId )
        {

        }

        public static void Add( string appid, wx_templates tmpl )
        {

        }



        /// <summary>
        /// 批量更新模板库
        /// </summary>
        /// <param name="appid">公众号id</param>
        /// <param name="tmpls">模板列表</param>
        /// <returns></returns>
        public static bool Update( string appid, List<wx_templates> tmpls )
        {
            testEntities db = new testEntities( );

            var old_tmpls = db.wx_templates.Where( qu => qu.appid == appid );
            db.wx_templates.RemoveRange( old_tmpls );
            try
            {
                db.SaveChanges( );
            }
            catch ( Exception ex )
            {
                Log.Logger.Log( "[dal: 删除模板库失败] " + ex.InnerException + "|" + ex.Message );
                return false;
            }

            db.wx_templates.AddRange( tmpls );
            try
            {
                db.SaveChanges( );
                return true;
            }
            catch ( Exception ex )
            {
                Log.Logger.Log( "[dal: 添加模板库失败] " + ex.InnerException + "|" + ex.Message );
                return false;
            }
        }




    }
}
