//
//  NewsDetailsViewController.swift
//  GoodNews
//
//  Created by Merdin Kahrimanovic on 05/08/2024.
//

import Foundation
import UIKit
import WebKit

class NewsDetailsViewController: UIViewController {
    
    var article: Article!
    private var newsDetailsViewModel: NewsDetailsViewModel!
    @IBOutlet weak var webView: WKWebView!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        setupUI()
    }
    
    private func setupUI() {
        self.newsDetailsViewModel = NewsDetailsViewModel(article)
        
        self.navigationItem.largeTitleDisplayMode = .never
        self.title = self.newsDetailsViewModel.sourceName
        
        guard let url = self.newsDetailsViewModel.url,
              let newsDetailURL = URL(string: url) else {
            return
        }
        
        let request = URLRequest(url: newsDetailURL)
        self.webView.load(request)
    }
}
