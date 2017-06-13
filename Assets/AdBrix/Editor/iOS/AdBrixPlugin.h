//
//  AdPopcornSDKPlugin.h
//  IgaworksAd
//
//  Created by wonje,song on 2014. 1. 21..
//  Copyright (c) 2014년 wonje,song. All rights reserved.
//

#import <Foundation/Foundation.h>

#import "AdBrix/AdBrix.h"
#import "AdBrix/CrossPromotion.h"



@interface AdBrixPlugin : NSObject


+ (AdBrixPlugin *)sharedAdBrixPlugin;
+ (AdBrixCommerceProductModel *)makeProductFromJsonForCommerceV2:(NSString *)purchaseDataJsonString;
+ (NSArray *)makeProductsFromJsonForCommerceV2:(NSString *)purchaseDataJsonString;
+ (AdBrixCommerceProductCategoryModel *)makeCategoryFromStringForCommerceV2: (NSString *)categoryString;
@end
