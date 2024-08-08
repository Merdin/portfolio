//
//  NewsHeadlineTableViewCell.swift
//  GoodNews
//
//  Created by Merdin Kahrimanovic on 04/08/2024.
//

import Foundation
import UIKit

class NewsHeadlineTableViewCell: UITableViewCell {
    
    @IBOutlet weak var titleLabel: UILabel!
    @IBOutlet weak var descriptionLabel: UILabel!
    @IBOutlet weak var headlineImageView: UIImageView!
    
}

extension NewsHeadlineTableViewCell {
    func configure(viewModel: ArticleViewModel) {
        self.titleLabel.text = viewModel.title
        self.descriptionLabel.text = viewModel.description

        viewModel.image { image in
            self.headlineImageView.image = image
        }
    }
}
