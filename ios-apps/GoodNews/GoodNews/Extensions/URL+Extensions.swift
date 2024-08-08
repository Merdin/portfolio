//
//  URL+Extensions.swift
//  GoodNews
//
//  Created by Merdin Kahrimanovic on 04/08/2024.
//

import Foundation

extension URL {
    
    static func urlForTopHeadlines(for category: String) -> URL {
        // TODO: Get your API KEY from https://newsapi.org/
        let apiKey = "YOUR_API_KEY"
        return URL(string: "https://newsapi.org/v2/top-headlines?country=nl&category=\(category)&apiKey=\(apiKey)")!
    }
}
