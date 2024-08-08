//
//  NewsHeadlinesTableViewController.swift
//  GoodNews
//
//  Created by Merdin Kahrimanovic on 04/08/2024.
//

import UIKit

class NewsHeadlinesTableViewController: UITableViewController {
    
    private var categoryListViewModel: CategoryListViewModel!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        setupUI()
        populateHeadlinesAndArticles()
    }
    
    private func setupUI() {
        self.navigationController?.navigationBar.prefersLargeTitles = true
        
        self.tableView.tableHeaderView = UIView.viewForTableViewHeader(subtitle: Date.dateAsStringForTableViewHeader())
    }

    private func populateHeadlinesAndArticles() {
        
        CategoryService().getAllHeadlinesForAllCategories { [weak self] categories in
            self?.categoryListViewModel = CategoryListViewModel(categories: categories)
            self?.tableView.reloadData()
        }
    }
    
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        if segue.identifier == "NewsDetailsViewController" {
            prepareSegueForNewsDetails(segue)
        }
    }
    
    private func prepareSegueForNewsDetails(_ segue: UIStoryboardSegue) {
        guard let newsDetailsViewController = segue.destination as? NewsDetailsViewController else {
            fatalError("NewsDetailsViewController is not loaded")
        }
        
        guard let indexPath = tableView.indexPathForSelectedRow else {
            fatalError("Unable to get the selected row")
        }
        
        let articleViewModel = self.categoryListViewModel.categoryAtIndex(index: indexPath.section).articleAtIndex(indexPath.row)
        
        newsDetailsViewController.article = articleViewModel.article
    }
    
    override func tableView(_ tableView: UITableView, heightForHeaderInSection section: Int) -> CGFloat {
        return self.categoryListViewModel.heightForHeaderInSection(section)
    }
    
    override func tableView(_ tableView: UITableView, viewForHeaderInSection section: Int) -> UIView? {
        let name = self.categoryListViewModel.categoryAtIndex(index: section).name
        return UIView.viewForSectionHeader(title: name)
    }
    
    override func numberOfSections(in tableView: UITableView) -> Int {
        return self.categoryListViewModel == nil ? 0 : self.categoryListViewModel.numberOfSections
    }
    
    override func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return self.categoryListViewModel.numberOfRowsInSection(section)
    }
    
    override func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        guard let cell = tableView.dequeueReusableCell(withIdentifier: "NewsHeadlineTableViewCell", for: indexPath) as? NewsHeadlineTableViewCell else {
            fatalError("NewsHeadlineTableViewCell not found")
        }
        
        let articleViewModel = self.categoryListViewModel.categoryAtIndex(index: indexPath.section).articleAtIndex(indexPath.row)
        
        cell.configure(viewModel: articleViewModel)
        
        return cell
    }
}
