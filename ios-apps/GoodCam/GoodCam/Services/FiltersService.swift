//
//  FiltersService.swift
//  GoodCam
//
//  Created by Merdin Kahrimanovic on 08/08/2024.
//

import Foundation
import UIKit
import CoreImage

class FiltersService {
    
    private var context: CIContext
    
    init() {
        self.context = CIContext()
    }
    
    func applyFilter(filter: CIFilter, to inputImage: UIImage, completion: @escaping ((UIImage) -> ())) {
        let sourceImage = CIImage(image: inputImage)!
        filter.setValue(sourceImage, forKey: kCIInputImageKey)
        
        if let cgimg = self.context.createCGImage(filter.outputImage!, from: filter.outputImage!.extent) {
            let processedImage = UIImage(cgImage: cgimg, scale: inputImage.scale, orientation: inputImage.imageOrientation)
            completion(processedImage)
        }
    }
    
    static func all() -> [CIFilter] {
        let blurFilter = self.blurFilter()
        let halftoneFilter = self.halftoneFilter()
        let crystallizeFilter = self.crystallizeFilter()
        let monochromeFilter = self.monochromeFilter()
        let sepiaFilter = self.sepiaFilter()
        
        return [blurFilter, halftoneFilter, crystallizeFilter, monochromeFilter, sepiaFilter]
    }
    
    private static func blurFilter() -> CIFilter {
        let blurFilter = CIFilter(name: "CIGaussianBlur")!
        blurFilter.setValue(5.0, forKey: kCIInputRadiusKey)
        return blurFilter
    }
    
    private static func halftoneFilter() -> CIFilter {
        let halfToneFilter = CIFilter(name: "CICMYKHalftone")!
        halfToneFilter.setValue(5.0, forKey: kCIInputWidthKey)
        return halfToneFilter
    }
    
    private static func crystallizeFilter() -> CIFilter {
        let crystallizeFilter = CIFilter(name: "CICrystallize")!
        crystallizeFilter.setValue(5, forKey: kCIInputRadiusKey)
        return crystallizeFilter
    }
    
    private static func monochromeFilter() -> CIFilter {
        let monochromeFilter = CIFilter(name: "CIColorMonochrome")!
        monochromeFilter.setValue(CIColor(red: 0.7, green: 0.7, blue: 0.7), forKey: kCIInputColorKey)
        monochromeFilter.setValue(10, forKey: kCIInputIntensityKey)
        return monochromeFilter
    }
    
    private static func sepiaFilter() -> CIFilter {
        let sepiaFilter = CIFilter(name: "CISepiaTone")!
        sepiaFilter.setValue(10, forKey: kCIInputIntensityKey)
        return sepiaFilter
    }
}
