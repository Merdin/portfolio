//
//  Category.swift
//  GoodNews
//
//  Created by Merdin Kahrimanovic on 04/08/2024.
//

import Foundation

struct Category {
    
    let title: String
    let articles: [Article]
    
    static func all() -> [String] {
        return ["Business", "Entertainment", "General", "Sports"]
    }
}
