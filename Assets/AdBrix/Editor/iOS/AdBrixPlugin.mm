//
//  AdPopcornSDKPlugin.m
//  IgaworksAd
//
//  Created by wonje,song on 2014. 1. 21..
//  Copyright (c) 2014년 wonje,song. All rights reserved.
//

#import "AdBrixPlugin.h"


UIViewController *UnityGetGLViewController();

static AdBrixPlugin *_sharedInstance = nil; //To make AdBrixPlugin Singleton

@implementation AdBrixPlugin

+ (void)initialize
{
	if (self == [AdBrixPlugin class])
	{
		_sharedInstance = [[self alloc] init];
	}
}


+ (AdBrixPlugin *)sharedAdBrixPlugin
{
	return _sharedInstance;
}

- (id)init
{
	self = [super init];
    
    if (self)
    {
        
    }
	return self;
}

+ (NSString *)checkNilToBlankString:(id)target
{
    NSString *returnString = @"";
    if (!([target isEqual:[NSNull null]] || target == nil))
    {
        returnString = target;
    }
    
    return returnString;
}

+ (double)checkDoubleNilToZero:(id)target
{
    double returnDouble = 0.0f;
    if (!([target isEqual:[NSNull null]] || target == nil))
    {
        returnDouble = (double)[target doubleValue];
    }
    
    return returnDouble;
}

+ (NSInteger)checkIntegerNilToZero:(id)target
{
    NSInteger returnInteger = 0;
    if (!([target isEqual:[NSNull null]] || target == nil))
    {
        returnInteger = [target integerValue];
    }
    
    return returnInteger;
}

+ (AdBrixCommerceProductModel *)makeProductFromJsonForCommerceV2:(NSString *)purchaseDataJsonString
{
    try {
        
        NSString *_productId = @"";
        NSString *_productName = @"";
        double _price = 0.0;
        double _discount = 0.0;
        NSUInteger _quantity = 1;
        NSString *_currency = @"";
        AdBrixCommerceProductCategoryModel *_categories;
        NSMutableDictionary *_extraAttrs;
        
        id dict=[NSJSONSerialization JSONObjectWithData:[purchaseDataJsonString dataUsingEncoding:NSUTF8StringEncoding] options:kNilOptions error:nil];
        
        for (id element in dict)
        {
            for(NSString* key in element)
            {
                if(![key isKindOfClass:[NSNull class]])
                {
                    if ([key isEqualToString:@"productId"])
                    {
                        _productId = [self checkNilToBlankString : [element objectForKey:key]];
                    }
                    if ([key isEqualToString:@"productName"])
                    {
                        _productName = [self checkNilToBlankString : [element objectForKey:key]];
                    }
                    if ([key isEqualToString:@"price"])
                    {
                        _price = [self checkDoubleNilToZero : [element objectForKey:key]];
                    }
                    if ([key isEqualToString:@"discount"])
                    {
                        _discount = [self checkDoubleNilToZero : [element objectForKey:key]];
                    }
                    if ([key isEqualToString:@"quantity"])
                    {
                        _quantity = [self checkIntegerNilToZero : [element objectForKey:key]];
                    }
                    if ([key isEqualToString:@"currency"])
                    {
                        _currency = [self checkNilToBlankString : [element objectForKey:key]];
                    }
                    if ([key isEqualToString:@"category"])
                    {
                        NSString *categories[5];
                        NSString *pCategories = [self checkNilToBlankString : [element objectForKey:key]];
                        if (pCategories) {
                            NSArray* categoryList = [pCategories componentsSeparatedByString:@"."];
                            for (int i=0; i<categoryList.count; ++i)
                            {
                                categories[i] = categoryList[i];
                            }
                        }
                        _categories = [AdBrixCommerceProductCategoryModel create:categories[0] category2:categories[1] category3:categories[2] category4:categories[3] category5:categories[4]];
                    }
                    if ([key isEqualToString:@"extra_attrs"])
                    {
                        _extraAttrs = [element objectForKey:key];
                    }
                }
            }
        }
        return [AdBrix createCommerceProductModel:_productId
                                      productName:_productName
                                            price:_price
                                         discount:_discount
                                         quantity:_quantity
                                   currencyString:_currency
                                         category:_categories
                                    extraAttrsMap:[AdBrixCommerceProductAttrModel  create:_extraAttrs]];
    }
    catch (NSException *exception)
    {
        NSLog(@"fail to make product for iOS native : %@", exception);
    }
    return nil;
}

+ (NSArray *)makeProductsFromJsonForCommerceV2:(NSString *)purchaseDataJsonString
{
    try {
        
            NSString *_productId = @"";
            NSString *_productName = @"";
            double _price = 0.0;
            double _discount = 0.0;
            NSUInteger _quantity = 1;
            NSString *_currency = @"";
            AdBrixCommerceProductCategoryModel *_categories;
            NSMutableDictionary *_extraAttrs;
            NSMutableArray *productItemsArr = [NSMutableArray array];
        
            id dict=[NSJSONSerialization JSONObjectWithData:[purchaseDataJsonString dataUsingEncoding:NSUTF8StringEncoding] options:kNilOptions error:nil];
        
            for (id element in dict)
            {
                for(NSString* key in element)
                {
                    if(![key isKindOfClass:[NSNull class]])
                    {
                        if ([key isEqualToString:@"productId"])
                        {
                            _productId = [self checkNilToBlankString : [element objectForKey:key]];
                        }
                        if ([key isEqualToString:@"productName"])
                        {
                            _productName = [self checkNilToBlankString : [element objectForKey:key]];
                        }
                        if ([key isEqualToString:@"price"])
                        {
                            _price = [self checkDoubleNilToZero : [element objectForKey:key]];
                        }
                        if ([key isEqualToString:@"discount"])
                        {
                            _discount = [self checkDoubleNilToZero : [element objectForKey:key]];
                        }
                        if ([key isEqualToString:@"quantity"])
                        {
                            _quantity = [self checkIntegerNilToZero : [element objectForKey:key]];
                        }
                        if ([key isEqualToString:@"currency"])
                        {
                            _currency = [self checkNilToBlankString : [element objectForKey:key]];
                        }
                        if ([key isEqualToString:@"category"])
                        {
                            NSString *categories[5];
                            NSString *pCategories = [self checkNilToBlankString : [element objectForKey:key]];
                            if (pCategories) {
                                NSArray* categoryList = [pCategories componentsSeparatedByString:@"."];
                                for (int i=0; i<categoryList.count; ++i)
                                {
                                    categories[i] = categoryList[i];
                                }
                            }
                            _categories = [AdBrixCommerceProductCategoryModel create:categories[0] category2:categories[1] category3:categories[2] category4:categories[3] category5:categories[4]];
                        }
                        if ([key isEqualToString:@"extra_attrs"])
                        {
                            _extraAttrs = [element objectForKey:key];
                        }
                    }
                }
                
                [productItemsArr addObject:[AdBrix createCommerceProductModel:_productId
                                                                  productName:_productName
                                                                        price:_price
                                                                     discount:_discount
                                                                     quantity:_quantity
                                                               currencyString:_currency
                                                                     category:_categories
                                                                extraAttrsMap:[AdBrixCommerceProductAttrModel  create:_extraAttrs]]];
            }
        return productItemsArr;
    } 
    catch (NSException *exception)
    {
        NSLog(@"fail to make product for iOS native : %@", exception);
    }
    return nil;
}

+(AdBrixCommerceProductCategoryModel *)makeCategoryFromStringForCommerceV2: (NSString *)categoryString
{
    NSString *categories[5];
    if (categoryString) {
        NSArray* categoryList = [categoryString componentsSeparatedByString:@"."];
        for (int i=0; i<categoryList.count; ++i)
        {
            categories[i] = categoryList[i];
        }
    }
    
    return [AdBrixCommerceProductCategoryModel create:categories[0] category2:categories[1] category3:categories[2] category4:categories[3] category5:categories[4]];
}


// When native code plugin is implemented in .mm / .cpp file, then functions
// should be surrounded with extern "C" block to conform C function naming rules
extern "C" {
	
    
    void _FirstTimeExperience(const char* name)
    {
        [AdBrix firstTimeExperience:[NSString stringWithUTF8String:name]];
    }
    
    void _FirstTimeExperienceWithParam(const char* name, const char* param)
    {
        [AdBrix firstTimeExperience:[NSString stringWithUTF8String:name] param:[NSString stringWithUTF8String:param]];
    }
    
    void _Retention(const char* name)
    {
        [AdBrix retention:[NSString stringWithUTF8String:name]];
    }
    
    void _RetentionWithParam(const char* name, const char* param)
    {
        [AdBrix retention:[NSString stringWithUTF8String:name] param:[NSString stringWithUTF8String:param]];
    }
    
    void _Buy(const char* name)
    {
        [AdBrix buy:[NSString stringWithUTF8String:name]];
    }
    
    void _BuyWithParam(const char* name, const char* param)
    {
        [AdBrix buy:[NSString stringWithUTF8String:name] param:[NSString stringWithUTF8String:param]];
    }
    
    
    void _ShowViralCPINotice()
    {
        NSLog(@"AdBrixPlugin : _ShowViralCPINotice");
      
        [AdBrix showViralCPINotice:UnityGetGLViewController()];
    }
    
    void _SetCustomCohort(AdBrixCustomCohortType customCohortType, const char* filterName)
    {
        [AdBrix setCustomCohort:customCohortType filterName:[NSString stringWithUTF8String:filterName]];
    }
    
    void _CrossPromotionShowAD(const char* activityName)
    {
        [CrossPromotion showAD:[NSString stringWithUTF8String:activityName] parentViewController:UnityGetGLViewController()];
    }
    
    void _AdBrixPurchase (const char* orderId, const char* productId, const char* productName, double price, int quantity, const char* currencyString, const char* category)
    {
        NSLog(@"_AdBrixPurchase %s %s %s %f %d %s %s",
              orderId, productId, productName, price, quantity, currencyString, category);
        
        //        NSArray* categories = @[
        //            [NSString stringWithUTF8String:category1],
        //            [NSString stringWithUTF8String:category2],
        //            [NSString stringWithUTF8String:category3],
        //            [NSString stringWithUTF8String:category4],
        //            [NSString stringWithUTF8String:category5]
        //        ];
        
        [AdBrix purchase:[NSString stringWithUTF8String:orderId]
                         productId:[NSString stringWithUTF8String:productId]
                       productName:[NSString stringWithUTF8String:productName]
                             price:price
                          quantity:quantity
                    currencyString:[NSString stringWithUTF8String:currencyString]
                          category:[NSString stringWithUTF8String:category]];
    }
    
    void _AdBrixPurchaseWithJson (const char* jsonString)
    {
        NSLog(@"_AdBrixPurchaseWithJson %s ", jsonString);
        [AdBrix purchase:[NSString stringWithUTF8String:jsonString]];
    }
    
    void _AdBrixPurchaseList (const char *pArr[], int arrCnt)
    {
        NSMutableArray *pList = [NSMutableArray array];
        if(!pArr || arrCnt <= 0)
        {
            NSLog(@"_AdBrixPurchaseList error! Cnt of purchaseList shold be more than zero!");
            return;
        }
        
        for(int i = 0; i < arrCnt; i++)
        {
            NSString* purchaseInfo = [NSString stringWithUTF8String:pArr[i]];
            NSLog(@"_AdBrixPurchaseList[%d] %@ ", i, purchaseInfo);
            if (purchaseInfo)
            {
                NSArray* arrPurchaseInfo = [purchaseInfo componentsSeparatedByString:@"&"];
                if(!(arrPurchaseInfo.count == 7))
                {
                    NSLog(@"_AdBrixPurchaseList error! PurchaseInfo datum shold be orderId(String), productId(String), productName(String), price(double), quantity(Int), category(String - ex Game.Console.Ps4");
                    return;
                }
                [pList addObject:[AdBrix createItemModel:arrPurchaseInfo[0] productId:arrPurchaseInfo[1] productName:arrPurchaseInfo[2] price:[arrPurchaseInfo[3] doubleValue] quantity:[arrPurchaseInfo[4] integerValue] currencyString:arrPurchaseInfo[5] category:arrPurchaseInfo[6]]];
            }
        }
        
        [AdBrix purchaseList:pList];
        
    }
    
    void _CommerceV2Purchase (const char* productID, double price, const char* currency, const char* paymentMethod)
    {
        [AdBrix commercePurchase:[NSString stringWithUTF8String:productID] price: price currency:[NSString stringWithUTF8String:currency] paymentMethod:[NSString stringWithUTF8String:paymentMethod]];
    }
    
    void _CommerceV2PurchaseII (const char* orderID, const char* purchaseDataJsonString, double discount, double deliveryCharge, const char* paymentMethod)
    {
        /*
        NSLog(@"REsult %s, %s, %f, %f, %s", orderID, purchaseDataJsonString, discount, deliveryCharge, paymentMethod);
        NSLog(@"Result %f, %f, %ld, %@, %@, %@, %@, %@",
              pItem.getPrice,
              pItem.getDiscount,
              pItem.getQuantity,
              pItem.getProductId,
              pItem.getCategories,
              pItem.getExtraAttrs,
              pItem.getProductName,
              pItem.getCurrencyString);
         */
        
        [AdBrix commercePurchase:[NSString stringWithUTF8String :orderID] productsInfos: [AdBrixPlugin makeProductsFromJsonForCommerceV2: [NSString stringWithUTF8String:purchaseDataJsonString]] discount:discount deliveryCharge:deliveryCharge paymentMethod:[NSString stringWithUTF8String : paymentMethod]];
    }
    
    void _DeeplinkOpen (const char* deeplinkUrl)
    {
        [AdBrix commerceDeeplinkOpen:[NSString stringWithUTF8String :deeplinkUrl]];
    }
    
    void _ProductView (const char* purchaseDataJsonString)
    {
        [AdBrix commerceProductView:[AdBrixPlugin makeProductFromJsonForCommerceV2: [NSString stringWithUTF8String:purchaseDataJsonString]]];
    }
    
    void _Refund (const char* orderID, const char* purchaseDataJsonString, double penaltyCharge)
    {
        [AdBrix commerceRefund:[NSString stringWithUTF8String :orderID] product:[AdBrixPlugin makeProductFromJsonForCommerceV2: [NSString stringWithUTF8String:purchaseDataJsonString]] penaltyCharge:penaltyCharge];
    }
    
    void _RefundBulk (const char* orderID, const char* purchaseDataJsonString, double penaltyCharge)
    {
        [AdBrix commerceRefundBulk:[NSString stringWithUTF8String :orderID] productsInfos:[AdBrixPlugin makeProductsFromJsonForCommerceV2: [NSString stringWithUTF8String:purchaseDataJsonString]] penaltyCharge:penaltyCharge];
    }
    
    void _AddToCart (const char* purchaseDataJsonString)
    {
        [AdBrix commerceAddToCart:[AdBrixPlugin makeProductFromJsonForCommerceV2: [NSString stringWithUTF8String:purchaseDataJsonString]]];
    }
    
    void _AddToCartBulk (const char* purchaseDataJsonString)
    {
        [AdBrix commerceAddToCartBulk:[AdBrixPlugin makeProductsFromJsonForCommerceV2: [NSString stringWithUTF8String:purchaseDataJsonString]]];
    }
    
    void _Login (const char* usn)
    {
        [AdBrix commerceLogin:[NSString stringWithUTF8String:usn]];
    }
    
    void _AddToWishList (const char* purchaseDataJsonString)
    {
        [AdBrix commerceAddToWishList:[AdBrixPlugin makeProductFromJsonForCommerceV2: [NSString stringWithUTF8String:purchaseDataJsonString]]];
    }
    
    void _CategoryView (const char* categoryString)
    {
        [AdBrix commerceCategoryView:[AdBrixPlugin makeCategoryFromStringForCommerceV2: [NSString stringWithUTF8String:categoryString]]];
    }
    
    void _ReviewOrder (const char* orderID, const char* purchaseDataJsonString, double discount, double deliveryCharge)
    {
        [AdBrix commerceReviewOrder: [NSString stringWithUTF8String:orderID] product:[AdBrixPlugin makeProductFromJsonForCommerceV2: [NSString stringWithUTF8String:purchaseDataJsonString]] discount:discount deliveryCharge:deliveryCharge];
    }
    
    void _ReviewOrderBulk (const char* orderID, const char* purchaseDataJsonString, double discount, double deliveryCharge)
    {
        [AdBrix commerceReviewOrderBulk:[NSString stringWithUTF8String:orderID] productsInfos:[AdBrixPlugin makeProductsFromJsonForCommerceV2: [NSString stringWithUTF8String:purchaseDataJsonString]] discount:discount deliveryCharge:deliveryCharge];
    }
    
    void _PaymentView (const char* orderID, const char* purchaseDataJsonString, double discount, double deliveryCharge)
    {
        [AdBrix commercePaymentView:[NSString stringWithUTF8String:orderID] productsInfos:[AdBrixPlugin makeProductsFromJsonForCommerceV2: [NSString stringWithUTF8String:purchaseDataJsonString]] discount:discount deliveryCharge:deliveryCharge];
    }
    
    void _Search (const char* purchaseDataJsonString, const char* keyword)
    {
        [AdBrix commerceSearch:[AdBrixPlugin makeProductsFromJsonForCommerceV2: [NSString stringWithUTF8String:purchaseDataJsonString]] keyword:[NSString stringWithUTF8String:keyword]];
    }
    
    void _Share (const char* channel, const char* purchaseDataJsonString)
    {
        [AdBrix commerceShare:[NSString stringWithUTF8String:channel] product:[AdBrixPlugin makeProductFromJsonForCommerceV2: [NSString stringWithUTF8String:purchaseDataJsonString]]];
    }
    
    const char* _AdBrixCurrencyName (int currency)
    {
        NSString* str = [AdBrix currencyName:currency];
        
        char* res = (char*)malloc(str.length+1);
        strcpy(res, [str UTF8String]);
        
        return res;
    }
    
    const char* _AdBrixPaymentMethodName (int method)
    {
        NSString* str = [AdBrix paymentMethod:method];
        
        char* res = (char*)malloc(str.length+1);
        strcpy(res, [str UTF8String]);
        
        return res;
    }
    
    const char* _AdBrixSharingChannelName (int channel)
    {
        NSString* str = [AdBrix sharingChannel:channel];
        
        char* res = (char*)malloc(str.length+1);
        strcpy(res, [str UTF8String]);
        
        return res;
    }

}

@end

