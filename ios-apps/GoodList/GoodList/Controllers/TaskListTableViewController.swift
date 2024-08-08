//
//  TaskListTableViewController.swift
//  GoodList
//
//  Created by Merdin Kahrimanovic on 08/08/2024.
//

import UIKit

class TaskListTableViewController: UITableViewController, AddTaskViewControllerDelegate {
    
    private var tasks = [Task]()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        setupUI()
    }
    
    private func setupUI() {
        self.title = "GoodList"
        self.navigationController?.navigationBar.prefersLargeTitles = true
        
        let addTaskBarButton = UIBarButtonItem.barButtonItemForAddTask(target: self, selector: #selector(addTaskBarButtonPressed))
        
        self.navigationItem.rightBarButtonItem = addTaskBarButton
    }
    
    @objc private func addTaskBarButtonPressed() {
        let vc = AddTaskViewController()
        vc.delegate = self
        
        let navigationController = UINavigationController()
        navigationController.pushViewController(vc, animated: true)
        self.present(navigationController, animated: true)
    }
    
    func addTaskViewControllerDidSave(task: Task, viewController: UIViewController) {
        viewController.dismiss(animated: true).self
        self.tasks.append(task)
        
        DispatchQueue.main.async {
            self.tableView.reloadData()
        }
    }
    
    override func numberOfSections(in tableView: UITableView) -> Int {
        return 1
    }
    
    override func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return self.tasks.count
    }
    
    override func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let task = self.tasks[indexPath.row]
        
        var cell: TaskTableViewCell!
        
        cell = tableView.dequeueReusableCell(withIdentifier: "TaskTableViewCell") as? TaskTableViewCell
        
        if cell == nil {
            cell = TaskTableViewCell(style: .default, reuseIdentifier: "TaskTableViewCell")
        }
        
        cell.titleLabel.text = task.title
        cell.priorityView.setTitle(task.priority.displayTitle, for: .normal)
        
        return cell
    }
}
