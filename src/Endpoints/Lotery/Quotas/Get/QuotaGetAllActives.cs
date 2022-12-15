using System;
using UnsualLotery.Infra.Data;

namespace UnsualLotery.Endpoints.Lotery.Quotas.Get;

public class QuotaGetAllActives
{
    public static string Template => "/quota";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    //public static Delegate Handler => Action;

    //public async Task<IResult> Action(ApplicationDbContext context) {
    //    var quotas = context.Quotas;


    //}
}

