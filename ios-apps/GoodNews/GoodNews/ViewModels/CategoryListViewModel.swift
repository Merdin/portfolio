//
//  CategoryListViewModel.swift
//  GoodNews
//
//  Created by Merdin Kahrimanovic on 05/08/2024.
//

import Foundation
import UIKit

struct CategoryListViewModel {
    private(set) var categories: [Category]
}

extension CategoryListViewModel {
    var numberOfSections: Int {
        return self.categories.count
    }
    
    func numberOfRowsInSection(_ section: Int) -> Int {
        return self.categories[section].articles.count
    }
    
    func heightForHeaderInSection(_ section: Int) -> CGFloat {
        return 60.0
    }
}

extension CategoryListViewModel {
    
    func categoryAtIndex(index: Int) -> CategoryViewModel {
        return CategoryViewModel(name: categories[index].title, articles: categories[index].articles)
    }
    
    func articleForSectionAtIndex(section: Int, index: Int) -> ArticleViewModel {
        return categoryAtIndex(index: section).articleAtIndex(index)
    }
}
