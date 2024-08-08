//
//  Task.swift
//  GoodList
//
//  Created by Merdin Kahrimanovic on 08/08/2024.
//

import Foundation

enum Priority: Int {
    case high = 0
    case medium = 1
    case low = 2
}

extension Priority {
    var displayTitle: String {
        get {
            switch self {
            case .high: "High"
            case .medium: "Medium"
            case .low: "Low"
            }
        }
    }
}

struct Task {
    let title: String
    let priority: Priority
}
