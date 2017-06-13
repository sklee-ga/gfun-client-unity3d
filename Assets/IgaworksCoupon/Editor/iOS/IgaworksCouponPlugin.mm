//
//  AdPopcornSDKPlugin.m
//  IgaworksAd
//
//  Created by wonje,song on 2014. 1. 21..
//  Copyright (c) 2014년 wonje,song. All rights reserved.
//

#import "IgaworksCouponPlugin.h"


UIViewController *UnityGetGLViewController();

static IgaworksCouponPlugin *_sharedInstance = nil; //To make AdBrixPlugin Singleton

@implementation IgaworksCouponPlugin

@synthesize callbackHandlerName = _callbackHandlerName;



+ (void)initialize
{
	if (self == [IgaworksCouponPlugin class])
	{
		_sharedInstance = [[self alloc] init];
	}
}


+ (IgaworksCouponPlugin *)sharedIgaworksCouponPlugin
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


- (void)setIgaworksCouponDelegate
{
    [IgaworksCoupon shared].delegate = self;
}


#pragma mark - IgaworksCouponDelegate
- (void)igaworksCouponValidationDidComplete:(BOOL)result message:(NSString *)message
{
    
    NSString *resultMessage = [NSString stringWithFormat: @"%d,%@", result, message];
    
    NSLog(@"resultMessage : %@", resultMessage);
    
    UnitySendMessage([_callbackHandlerName UTF8String], "IgaworksCouponValidationDidComplete", [resultMessage UTF8String]);
}

// When native code plugin is implemented in .mm / .cpp file, then functions
// should be surrounded with extern "C" block to conform C function naming rules
extern "C" {
	
    void _IgaworksCouponSetCallbackHandler(const char* handlerName)
    {
        [[IgaworksCouponPlugin sharedIgaworksCouponPlugin] setCallbackHandlerName:[NSString stringWithUTF8String:handlerName]];
        NSLog(@"callbackHandlerName: %@", [[IgaworksCouponPlugin sharedIgaworksCouponPlugin] callbackHandlerName]);
    }
    
    
    // coupon
    void _ShowCoupon()
    {
        [[IgaworksCouponPlugin sharedIgaworksCouponPlugin] setIgaworksCouponDelegate];
        
        [IgaworksCoupon showCoupon];
        
    }
    
    void _CheckCoupon(const char* code)
    {
        [[IgaworksCouponPlugin sharedIgaworksCouponPlugin] setIgaworksCouponDelegate];
        
        if(code != NULL)
        {
            [IgaworksCoupon checkCoupon:[NSString stringWithUTF8String:code]];
        }
        else
        {
            [IgaworksCoupon checkCoupon:nil];
        }
    }

}

@end

