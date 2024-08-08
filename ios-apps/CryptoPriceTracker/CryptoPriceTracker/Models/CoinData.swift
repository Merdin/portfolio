//
//  CoinData.swift
//  CryptoPriceTracker
//
//  Created by Merdin Kahrimanovic on 08/08/2024.
//

import UIKit
import Alamofire

class CoinData {
    static let shared = CoinData()
    var coins = [Coin]()
    var delegate: CoinDataDelegate?
    
    private init() {
        let symbols = ["BTC", "ETH", "LTC"]
        
        for symbol in symbols {
            let coin = Coin(symbol: symbol, image: UIImage(named: symbol)!)
            coins.append(coin)
        }
    }
    
    func getPrices() {
        var listOfSymbols = ""
        
        for coin in coins {
            listOfSymbols += coin.symbol
            if coin.symbol != coins.last?.symbol {
                listOfSymbols += ","
            }
        }
        
        let response = AF.request("https://min-api.cryptocompare.com/data/pricemulti?fsyms=\(listOfSymbols)&tsyms=EUR").responseJSON { response in
            if let json = response.value as? [String: Any] {
                for coin in self.coins {
                    var coin = coin
                    if let coinJSON = json[coin.symbol] as? [String: Double] {
                        
                        if let price = coinJSON["EUR"] {
                            coin.price = price
                        }
                    }
                }
                
                self.delegate?.newPrices()
            }
        }
        
    }
}

protocol CoinDataDelegate {
    func newPrices()
}


class Coin {
    let symbol: String
    let image: UIImage
    var price: Double = 0.0
    let amount: Double = 0.0
    let history: [Double] = []
    
    init(symbol: String, image: UIImage) {
        self.symbol = symbol
        self.image = image
    }
    
    func priceAsString() -> String {
        if price.isZero {
            return "Loading..."
        }
        
        let formatter = NumberFormatter()
        formatter.locale = Locale(identifier: "nl_NL")
        formatter.numberStyle = .currency
        if let price = formatter.string(from: NSNumber(floatLiteral: price)) {
            return price
        } else {
            return "ERROR"
        }
    }
}
