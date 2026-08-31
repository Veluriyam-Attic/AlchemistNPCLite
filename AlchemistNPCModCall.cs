using AlchemistNPCLite.NPCs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AlchemistNPCLite;

public static class AlchemistNPCModCall
{
    // this is for other mod to add new items to shop
    private static string[] _operatorModShops = [Operator.ModMaterialShop,Operator.Bags1Shop,Operator.Bags2Shop,Operator.Bags3Shop];
    /* // this is for other mod hide some item or modify some item's price or condition
    // shouldn't add CustomShop to it
    private static string[] _operatorAllShops = [Operator.MaterialShop, Operator.ModMaterialShop, Operator.VanillaBagsShop, Operator.Bags1Shop, Operator.Bags2Shop, Operator.Bags3Shop]; */ // YuBell: I think it's not nesseary
    public const string failed = "AlchemistNPCLite:Failed To Add Item To Opertator";
    public const string successed = "AlchemistNPCLite:Successfully Add Item To Opertator";

    public static string AddItemToOperator(string shop, Mod source,string name,int price,Condition[] conditions)
    {
        if (!_operatorModShops.Contains(shop))
            return failed;

        if (Operator.ModCall_AddItem.Keys.Contains(shop))
        {
            if (!Operator.ModCall_AddItem.TryGetValue(shop,out List<(Mod, string, int, Condition[])> list))
                return failed;

            list.Add((source, name, price, conditions));
        }
        else
        {
            if (!Operator.ModCall_AddItem.TryAdd(shop, new List<(Mod source, string name, int price, Condition[] conditions)>() { (source, name, price, conditions) }))
                return failed;
        }

        return successed;
    }
}
