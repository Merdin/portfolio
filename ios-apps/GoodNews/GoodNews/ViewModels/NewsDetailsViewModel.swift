//
//  NewsDetailsViewModel.swift
//  GoodNews
//
//  Created by Merdin Kahrimanovic on 05/08/2024.
//

import Foundation

struct NewsDetailsViewModel {
    let article: Article
}

extension NewsDetailsViewModel {
    init(_ article: Article) {
        self.article = article
    }
}

extension NewsDetailsViewModel {
    var sourceName: String {
        return self.article.sourceName
    }
    
    var url: String? {
        return self.article.url
    }
}
