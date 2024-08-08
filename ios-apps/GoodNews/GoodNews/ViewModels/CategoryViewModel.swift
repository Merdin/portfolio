//
//  CategoryViewModel.swift
//  GoodNews
//
//  Created by Merdin Kahrimanovic on 05/08/2024.
//

import Foundation


struct CategoryViewModel {
    let name: String
    let articles: [Article]
}

extension CategoryViewModel {
    func articleAtIndex(_ index: Int) -> ArticleViewModel {
        let article = self.articles[index]
        return ArticleViewModel(article)
    }
}
