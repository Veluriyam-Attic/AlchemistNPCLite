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
    /* 
    This is a Mod Call Examle
——————————————————————————————————————————————————————————————————————————————————————————
    build.txt => sortAfter = AlchemistNPCLite

    public void override Load()
    {
        if(ModLoader.TryGetMod("AlchemistNPCLite",out Mod ANPCLite))
        {
            ANPCLite.Call("Add Item To Operator","target shop name",YourMod,"your item's internal name",price,Condition[])
        }
    }
——————————————————————————————————————————————————————————————————————————————————————————
    *target shop name*
    this can only be "ModMaterials"/"ModBags1"/"ModBags2"/"ModBags3"/

    You can add your materials to ModMaterials

    ModBags1 is already added Calamity[25] & lots of Affiliated Mods(Catalyst[1] Entropy(added by itself) CalamityHunt[1]) //Entropy will be rename to Call of Void
        total:27+Entropy
    ModBags2 is already added Fargo[8] & Thorium[11] & Ancient Awaken[3]
        total:22
    ModBags3 is already added Redemption[10]
        total:10
——————————————————————————————————————————————————————————————————————————————————————————
    *Condition[]*
    the condition must be Condition[],can not be Condition！！！
——————————————————————————————————————————————————————————————————————————————————————————
    */


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
