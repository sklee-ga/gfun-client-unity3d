//
//  AdPopcornSDKPlugin.m
//  IgaworksAd
//
//  Created by wonje,song on 2014. 1. 21..
//  Copyright (c) 2014년 wonje,song. All rights reserved.
//

#import "IgaworksNanooPlugin.h"


UIViewController *UnityGetGLViewController();

static IgaworksNanooPlugin *_sharedInstance = nil; //To make AdBrixPlugin Singleton

@implementation IgaworksNanooPlugin

@synthesize callbackHandlerName = _callbackHandlerName;



+ (void)initialize
{
	if (self == [IgaworksNanooPlugin class])
	{
		_sharedInstance = [[self alloc] init];
	}
}


+ (IgaworksNanooPlugin *)sharedIgaworksNanooPlugin
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


- (void)setIgaworksNanooDelegate
{
    [IgaworksNanoo shared].delegate = self;
}


#pragma mark - IgaworksNanooDelegate
// getNanooFanPage 요청이 성공하면, nanoo fan page url을 return합니다.
- (void)getNanooFanPageDidComplete:(NSString *)nanooFanPageurl
{
    NSLog(@"getNanooFanPageDidComplete : nanooFanPageurl : %@", nanooFanPageurl);
    
    UnitySendMessage([_callbackHandlerName UTF8String], "GetNanooFanPageDidComplete", [nanooFanPageurl UTF8String]);
}

//  getNanooFanPage 요청이 실패한경우, error를 return합니다.
- (void)getNanooFanPageFailedWithError:(NSError *)error
{
    NSLog(@"getNanooFanPageFailedWithError : %@", [error localizedDescription]);
    
    UnitySendMessage([_callbackHandlerName UTF8String], "GetNanooFanPageFailedWithError", [[error localizedDescription] UTF8String]);
}


/*!
 @abstract
 naoo fan page가 열리기 전에 호출된다.
 
 @discussion
 */
- (void)willOpenNanooFanPage
{
    NSLog(@"willOpenNanooFanPage.");
    
    UnitySendMessage([_callbackHandlerName UTF8String], "WillOpenNanooFanPage", "success");
}

/*!
 @abstract
 naoo fan page가 열린직 후 호출된다.
 
 @discussion
 */
- (void)didOpenNanooFanPage
{
    NSLog(@"didOpenNanooFanPage.");
    
    UnitySendMessage([_callbackHandlerName UTF8String], "DidOpenNanooFanPage", "success");
}


/*!
 @abstract
 naoo fan page가 닫히기 전에 호출된다.
 
 @discussion
 */
- (void)willCloseNanooFanPage
{
    NSLog(@"willCloseNanooFanPage.");
    
    UnitySendMessage([_callbackHandlerName UTF8String], "WillCloseNanooFanPage", "success");
}

/*!
 @abstract
 naoo fan page가 닫힌직 후 호출된다.
 
 @discussion
 */
- (void)didCloseNanooFanPage
{
    NSLog(@"didCloseNanooFanPage.");
    
    UnitySendMessage([_callbackHandlerName UTF8String], "DidCloseNanooFanPage", "success");
}

// When native code plugin is implemented in .mm / .cpp file, then functions
// should be surrounded with extern "C" block to conform C function naming rules
extern "C" {
	
    void _IgaworksNanooSetCallbackHandler(const char* handlerName)
    {
        [[IgaworksNanooPlugin sharedIgaworksNanooPlugin] setCallbackHandlerName:[NSString stringWithUTF8String:handlerName]];
        NSLog(@"callbackHandlerName: %@", [[IgaworksNanooPlugin sharedIgaworksNanooPlugin] callbackHandlerName]);
    }
    
    void _GetNanooFanPage()
    {
        [[IgaworksNanooPlugin sharedIgaworksNanooPlugin] setIgaworksNanooDelegate];
        
        [IgaworksNanoo getNanooFanPage:UnityGetGLViewController()];
    }

}

@end

