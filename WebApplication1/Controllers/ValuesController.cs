using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
   // [Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
       // public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        //public string Get(int id)
        //{
        //    return (id*id).ToString();
        //}

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }


        // [HttpGet]

        //public HttpResponseMessage GetVakifListByPaging(int pageNumber = 1, int pageSize = 10, string filter = "")

        //{

        //    try

        //    {

        //        using (Entities db = new Entities())

        //        {

        //            IQueryable<BUTUNLESIK_INFO> query = db.BUTUNLESIK_INFO.AsQueryable();

        //            if (!string.IsNullOrEmpty(filter))

        //                query = query.Where(vakif => vakif.VAKIF_ILI.Contains(filter)).AsQueryable();



        //            var vakifList = query.OrderBy(a => a.VAKIF_ILI).ThenBy(a => a.VAKIF_ILCESI).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        //            return Request.CreateResponse(HttpStatusCode.OK, vakifList);

        //        }

        //    }

        //    catch (Exception e)

        //    {

        //        return Request.CreateResponse(HttpStatusCode.NotFound);

        //    }

        //}





        [HttpGet]

        public HttpResponseMessage GetVakifListByPagedList(int pageNumber = 1, int pageSize = 10, string cityFilter = "", string foundationFilter = "")

        {
            try
            {
                using (CONTOSOEntities db = new CONTOSOEntities())
                {
                    IQueryable<Foundation> query = db.Foundation.AsQueryable();

                    if (!string.IsNullOrEmpty(cityFilter))

                        query = query.Where(vakif => vakif.Province.ProvinceName.Contains(cityFilter)).AsQueryable();

                    if (!string.IsNullOrEmpty(foundationFilter))

                        query = query.Where(vakif => vakif.County.CountyName.Contains(foundationFilter)).AsQueryable();

                    query = query.OrderBy(a => a.Province.ProvinceName).ThenBy(a => a.County.CountyName);

                    PagedList<Foundation> returnList = new PagedList<Foundation>(query, pageNumber, pageSize);

                    IEnumerable<FoundationViewModel> fvm = new List<FoundationViewModel>();
                 
                    fvm = returnList.Select(f => new FoundationViewModel {
                        ID=f.ID,
                        FName = f.FName,
                        CountyName = f.County.CountyName,
                        ProvinceName = f.Province.ProvinceName,
                        Address = f.Address,
                        Phone = f.Phone,
                        Fax = f.Fax,
                        EMail = f.EMail
                    });

                    PagedList<FoundationViewModel> list = new PagedList<FoundationViewModel>(fvm, returnList.PagingMetaData);

                    PagedListContainer<FoundationViewModel> pgList = new PagedListContainer<FoundationViewModel>(list);
                    return Request.CreateResponse(HttpStatusCode.OK, pgList);
                }
            }

            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
        }
    }
}
