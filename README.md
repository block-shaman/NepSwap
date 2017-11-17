# NepSwap

White Paper

This is Protocol for trading NEP based assets, in near future, it will be able to process, orderbook, deposits/withdrawals, trade, placeOrder and many more exciting things that a respected exchange protocol would have. 
At first it will be a simple contract, which will be able to swap tokens between users, keep orders queue, swap with contrat's value (if it has some, if not swap with users or place in a queue). 
Every transaction will take 1-3x of 0.001 GAS, so it will be up to users to pay and completely their choice to trade.

NepSwap, will not have any tokens most likely, Assets will be traded to NEO, and then, in the next update, assets will be tradable to each other, each way.

NepSwap is something inbetween Bancor Protocol and Etherdelta on Ethereum blockchain.
Neo is helping us developers, to start programming smart contract in no time, and process thousands of transactions per block, which makes projects like NepSwap, a lot more possible, secure and flexible.
So far only couple of basic functions has been ported to .NET Core.
Full port is expected to be completed within 1-2 weeks, as the whole project took roughly 3 days in total, due to the lack of personal time.


Short Documentation:

  - public static bool withdrawAsset (string name, ulong quantity)
 
      Query contract to withdraw asset, if user/caller has this asset and this amount in accountBag, it will return true and send/refund the asset.
      
  - public static bool depositAsset()
  
    Send asset to contract using depostAsset method. It will figure out whether user/caller has actually sent anything, and what type of asset, receive asset, update user properties in accountBag

  To be continued:
  - updateOrderBook()
  - updateWaitList()
  ... and more

Project Rough Pseudo-code

**********************************************
**********************************************
var orders[] = new Array();
var waitList[] = new Array();

updateDisplay();

transaction made ();

getPrice( asset ) {
	return waitList[asset].lastPrice;
}

updateWaitList()
{
	for each account in waitList
		for each asset in waitList[account]
			if waitList[account][asset].balance > 0
			{
				if ( contract[asset].balance > waitList[account].quantity )
				{
				contract.sendTo( account, asset, quantity );
				waitList.remove( account, asset );
				}
			}

}

depositAsset ( name, quantity )
{
	if name != NEO
	{
		contract.balance.asset += quantity
		account.asset.initialValue = quantity
	} else {
		contract.balance.neo += transaction.value;

	}
}

wthdrawAsset ( name, quantity )
{
	if account.asset.initialValue > quantity
		if name != GAS or NEO {
			if contract.balance.asset < quantity
				contract.sendTo(asset, quantity)
			} else {
				print ( balance unavailable at the moment, wait or cancel? )
					var readInput;
						if readInput = wait
							updateWaitList(asset, quantity)
						else if readInput = cancel
							print ( withdrawal of @asset has been canceled by user )
							return;
			}

}

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


main () {
	var genesisBlock = getCurrentBlock;
	var nextRound = genesisBlock + 1000;

	while genesisBlock < nextRound
		do something
	done

	genesisBlock = nextRound;
	nextRound += 1000;
}
**********************************************
**********************************************
