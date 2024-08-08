//
//  ThemeManager.swift
//  GoodList
//
//  Created by Merdin Kahrimanovic on 08/08/2024.
//

import UIKit

class ThemeManager {
    static func setup() {
        setupUINavigationBar()
    }
    
    private static func setupUINavigationBar() {        
        let appearance = UINavigationBarAppearance()
        appearance.configureWithOpaqueBackground()
        appearance.backgroundColor = UIColor(displayP3Red: 142/255, green: 68/255, blue: 173/255, alpha: 1.0)
        appearance.titleTextAttributes = [.foregroundColor: UIColor.white]
        appearance.largeTitleTextAttributes = [.foregroundColor: UIColor.white]
        
        
        UINavigationBar.appearance().standardAppearance = appearance
        UINavigationBar.appearance().scrollEdgeAppearance = appearance
        UINavigationBar.appearance().compactAppearance = appearance
        UINavigationBar.appearance().tintColor = .white
    }
}
