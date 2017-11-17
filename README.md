# NepSwap

# White Paper

This is Protocol for trading NEP based assets, in near future, it will be able to process, orderbook, deposits/withdrawals, trade, placeOrder and many more exciting things that a respected exchange protocol would have. 
At first it will be a simple contract, which will be able to swap tokens between users, keep orders queue, swap with contrat's value (if it has some, if not swap with users or place in a queue). 
Every transaction will take 1-3x of 0.001 GAS, so it will be up to users to pay and completely their choice to trade.

NepSwap, will not have any tokens most likely, Assets will be traded to NEO, and then, in the next update, assets will be tradable to each other, each way.

NepSwap is something inbetween Bancor Protocol and Etherdelta on Ethereum blockchain.
Neo is helping us developers, to start programming smart contract in no time, and process thousands of transactions per block, which makes projects like NepSwap, a lot more possible, secure and flexible.
So far only couple of basic functions has been ported to .NET Core.
Full port is expected to be completed within 1-2 weeks, as the whole project took roughly 3 days in total, due to the lack of personal time.


# Short Documentation:

  - public static bool withdrawAsset (string name, ulong quantity)
 
      Query contract to withdraw asset, if user/caller has this asset and this amount in accountBag, it will return true and send/refund the asset.
      
  - public static bool depositAsset()
  
    Send asset to contract using depostAsset method. It will figure out whether user/caller has actually sent anything, and what type of asset, receive asset, update user properties in accountBag

  To be continued:
  - updateOrderBook()
  - updateWaitList()
  ... and more

Please see Pseudo Code and Code Base
