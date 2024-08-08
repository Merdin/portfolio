//
//  FiltersScrollView.swift
//  GoodCam
//
//  Created by Merdin Kahrimanovic on 08/08/2024.
//

import Foundation
import UIKit
import CoreImage

protocol FiltersScrollViewDelegate {
    func filterScorllViewDidSelectFilter(filter: CIFilter)
}

class FiltersScrollView: UIScrollView {
    
    var filterDelegate: FiltersScrollViewDelegate?
    private var filtersService: FiltersService!
    
    init(parentView: UIView, frame: CGRect = CGRect.zero) {
        super.init(frame: frame)
        commonInit()
    }
    
    required init?(coder: NSCoder) {
        super.init(coder: coder)
        commonInit()
    }
    
    private func commonInit() {
        self.filtersService = FiltersService()
        setupFilters()
    }
    
    private func registerTapGestureRecognizer(for view: UIView) {
        let tapGestureRecognizer = UITapGestureRecognizer(target: self, action: #selector(tapped))
        view.addGestureRecognizer(tapGestureRecognizer)
    }
    
    @objc func tapped(recognizer: UITapGestureRecognizer) {
        guard let selectedFilterImageView = recognizer.view as? UIImageView else {
            return
        }
        
        self.filterDelegate?.filterScorllViewDidSelectFilter(filter: FiltersService.all()[selectedFilterImageView.tag])
    }
    
    private func setupFilters() {
        
        var offset: CGFloat = 10.0
        
        for (index, filter) in FiltersService.all().enumerated() {
            
            let filterImageView = UIImageView.imageForFilterView()
            registerTapGestureRecognizer(for: filterImageView)
            self.addSubview(filterImageView)
            filterImageView.tag = index
            filterImageView.isUserInteractionEnabled = true
            filterImageView.frame.origin.x = offset
            filterImageView.center.y = self.frame.height/2
            
            offset += filterImageView.frame.width + filterImageView.frame.width/4
            self.contentSize = CGSize(width: offset, height: self.frame.height)
            
            self.filtersService.applyFilter(filter: filter, to: filterImageView.image!) {
                filterImageView.image = $0
            }
        }
    }
}
