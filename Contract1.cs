using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using System;
using System.Numerics;
using System.ComponentModel;

namespace NepSwap
{
    //public class AssetObject{};
    public class NepSwapClass : SmartContract
    {
        public static AssetObject[] assetList;
        //static List<string> orderList = new List<string>();
        //public static List<AssetObject> assetList;
        private static readonly byte[] neo_asset_id = {
                                155, 124, 255, 218, 166, 116, 190, 174, 15, 147, 14, 190, 96, 133, 175, 144, 147, 229,
                                254, 86, 179, 74, 92, 34, 12, 205, 207, 110, 252, 51, 111, 197 };

        private static readonly byte[] contractHash = { 2, 133, 234, 182, 95, 74, 1, 38, 228, 184, 91, 78, 93, 139, 126,
                                48, 58, 255, 126, 251, 54, 13, 89, 95, 46, 49, 137, 187, 144, 72, 122, 213, 170 };


        //[DisplayName("transfer")]
        //public static event Action<byte[], byte[], BigInteger> Transferred;

        //[DisplayName("refund")]
        //public static event Action<byte[], BigInteger> Refund;
        public class OrderObject
        {
            public byte[] user;
            public BigInteger price;
            public BigInteger amount;
        }

        public class AccountObject
        {
            public byte[] address;
            public BigInteger amount;
        }

        public class AssetObject
        {
            public string name;
            public ulong lastPrice;
            public string[] waitList;
            public AccountObject[] accountBag; //only temporary solution, for several dozens of users
            public OrderObject[] asksBook;
            public OrderObject[] bidsBook;

            public AssetObject() {
                waitList = new string[]{ "testUser", "testAmont" };
            }
        }

        private static void Initialise()
        {
            //assetList = { }
            assetList[0] = (
                new AssetObject
                {
                    name = "NEO",
                    lastPrice = 0
                });
        }

        public static ulong getPrice(string asset)
        {
            if (asset == "NEO")                
                return assetList[0].lastPrice;
            else // next update: multi asset compatibility
                return 0;
        }
        
        // Can't accept multiple assests at once
        // will return false and refund, unless it returns true somewhere
        public static bool depositAsset()
        {
            Transaction tx = (Transaction)ExecutionEngine.ScriptContainer;
            TransactionOutput[] outputs = tx.GetOutputs();
            ulong value = 0; BigInteger previousValue;
            
            foreach (TransactionOutput output in outputs)
            {
                // To do: next update: switch, enaums, multi-asset support

                if (output.ScriptHash == GetReceiver() && output.AssetId == neo_asset_id)
                {
                    if (output.ScriptHash != null)
                    {
                        value += (ulong)output.Value;

                        foreach (AccountObject array in assetList[0].accountBag)
                        {
                            if (array.address == output.ScriptHash && value > 0)
                            {
                                previousValue = array.amount;           //if (!assetList[0].accountBag.TryGetValue(output.ScriptHash, out previousValue)) { }
                                array.amount += value;                   //assetList[0].accountBag.TryUpdate(output.ScriptHash, value, previousValue);
                                return true;
                            }
                        }
                    }
                }
            }
            return false; // wrong asset or amount
        }

        public static bool withdrawAsset (string name, ulong quantity )
        {
            Transaction tx = (Transaction)ExecutionEngine.ScriptContainer;
            TransactionOutput[] outputs = tx.GetOutputs();
            Account account = Blockchain.GetAccount(contractHash);
            byte[] sender = GetSender();
            //BigInteger previousValue;

            foreach (TransactionOutput output in outputs)
            {
                foreach (AccountObject array in assetList[0].accountBag)
                {
                    if (array.address == sender)
                        // if user has this amount in AccountBag and if Contract has this amount
                        if (array.amount < quantity )
                            if (quantity < (ulong)account.GetBalance(neo_asset_id))
                            {
                                ulong contribute_value = GetContributeValue();
                                if (contribute_value > 0 && sender.Length != 0)
                                {
                                    array.amount -= quantity; //update accountBag record

                                    //Refund(sender, contribute_value); //contract.sendTo(asset, quantity)

                                    return true;    //Next Update: Refund / Withdraw / Transfer asset back

                                }
                            } else
                            {
                                //updateWaitList(asset, quantity)
                                return false;
                            }
                }
            }
            return false;
        }

        /* ********** PSEUDO CODE *********************************************
         updateOrderBook( asset, price, quantity, duration, user )
        {
	        var buy[] = new Array();   // in descending order
	        var sell[] = new Array();  // in ascending order
	        for each asset in orders
	        {
		        for each item in orders[asset]
			        var lastPrice = getPrice( asset );
			        if orders[assets][item].price <= lastPrice
			        {
				        buy[item].push( item );

			        } else if orders[assets][item].price > lastPrice
			        {
				        sell[item].push( item );
			        }
	        }

        }

        postOrder ( asset, price, quantity, duration )
        {
	        if account.asset.initialValue > quantity
			        account.depositAsset( asset, quantity )
			        if orders[account][asset].price != price
				        contracts.orders.add ( transaction.user.last.asset, transaction.user.last,price,
																							        transaction.user.last.quantity, transaction.user, duration )						//updateOrderBook( asset, price, quantity, duration, user )
	        else
		        print ( not enough balance )

        }

        updateAsset ( asset, user, quantity )
        {
	        if (contract.balance.asset.quantity > quantity) {
		        contract.sendTo( asset, user, quantity );
	        } else {
		        if !(queue.contains(account, asset))
		        {
			        contract.updateWaitList(asset, quantity)
		        }
	        }
        }

        trade ( asset, price, user, quantity ... )
        {
	        if account.asset.initialValue > quantity
	        {
		        account.depositAsset( asset, quantity );
		        contract.sendTo( asset, user, quantity );
	        } else {
		        print ( not enough @asset );
	        }
        }

        globalUpdate ()
        {
	        for each account (has * asset)
		        if account has balance 0f asset
			        if asset != NEO or asset != GAS
					        get account.initialValue
					        if asset >< initialValue
						        update account.initialValue
						        send NEO * (price) to account from contract

        }
        ****************** END OF PSEUDO CODE *********************************************
        */


        public static bool Main()
        {
            Initialise();
            
            return true;
        }

        // Methods from Neo NEP1 Template
        private static ulong GetContributeValue()
        {
            Transaction tx = (Transaction)ExecutionEngine.ScriptContainer;
            TransactionOutput[] outputs = tx.GetOutputs();
            ulong value = 0;
            // get the total amount of Neo
            foreach (TransactionOutput output in outputs)
            {
                if (output.ScriptHash == GetReceiver() && output.AssetId == neo_asset_id)
                {
                    value += (ulong)output.Value;
                }
            }
            return value;
        }

        private static byte[] GetSender()
        {
            Transaction tx = (Transaction)ExecutionEngine.ScriptContainer;
            TransactionOutput[] reference = tx.GetReferences();
            // refund will proceed via depositAsset method
            foreach (TransactionOutput output in reference)
            {
                if (output.AssetId == neo_asset_id) return output.ScriptHash;
            }
            return new byte[0];
        }

        private static byte[] GetReceiver()
        {
            return ExecutionEngine.ExecutingScriptHash;
        }

    }
}
