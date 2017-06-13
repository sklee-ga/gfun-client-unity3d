//
//  AdPopcornOfferwallPlugin.m
//  AdPopcornOfferwallPlugin
//
//  Created by wonje,song on 2014. 1. 21..
//  Copyright (c) 2014년 wonje,song. All rights reserved.
//

#import "AdPopcornOfferwallPlugin.h"


UIViewController *UnityGetGLViewController();

static AdPopcornOfferwallPlugin *_sharedInstance = nil; //To make IgaworksCorePlugin Singleton

@implementation AdPopcornOfferwallPlugin

@synthesize callbackHandlerName = _callbackHandlerName;
@synthesize userDataDictionaryForFilter = _userDataDictionaryForFilter;


+ (void)initialize
{
	if (self == [AdPopcornOfferwallPlugin class])
	{
		_sharedInstance = [[self alloc] init];
	}
}


+ (AdPopcornOfferwallPlugin *)sharedAdPopcornOfferwallPlugin
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



- (void)setAdPopcornOfferwallDelegate
{
    [AdPopcornOfferwall shared].delegate = self;
}


- (void)setUserDataDictionaryForFilter:(NSString *)key value:(NSString *)value
{
    if (!_userDataDictionaryForFilter)
    {
        _userDataDictionaryForFilter = [[NSMutableDictionary alloc] init];
    }
    
    [_userDataDictionaryForFilter setObject:value forKey:key];
}

#pragma mark - AdPopcornOfferwallDelegate
- (void)willOpenOfferWall
{
    UnitySendMessage([_callbackHandlerName UTF8String], "OfferWallWillOpen", "success");
}


- (void)didOpenOfferWall
{
    UnitySendMessage([_callbackHandlerName UTF8String], "OfferWallOpened", "success");
}

- (void)willCloseOfferWall
{
    UnitySendMessage([_callbackHandlerName UTF8String], "OfferWallWillClose", "success");
}

- (void)didCloseOfferWall
{
    UnitySendMessage([_callbackHandlerName UTF8String], "OfferWallClosed", "success");
}

- (void)willOpenPromotionEvent
{
    UnitySendMessage([_callbackHandlerName UTF8String], "PromotionEventWillOpen", "success");
}

- (void)didOpenPromotionEvent
{
    UnitySendMessage([_callbackHandlerName UTF8String], "PromotionEventOpened", "success");
}

- (void)willClosePromotionEvent
{
    UnitySendMessage([_callbackHandlerName UTF8String], "PromotionEventWillClose", "success");
}

- (void)didClosePromotionEvent
{
    UnitySendMessage([_callbackHandlerName UTF8String], "PromotionEventClosed", "success");
}

- (void)loadVideoAdSuccess
{
    UnitySendMessage([_callbackHandlerName UTF8String], "LoadVideoAdSuccess", "success");
}

- (void)loadVideoAdFailedWithError:(APError *)error
{
    UnitySendMessage([_callbackHandlerName UTF8String], "LoadVideoAdFailedWithError", [[error description] UTF8String]);
}

- (void)willOpenVideoAd
{
    UnitySendMessage([_callbackHandlerName UTF8String], "WillOpenVideoAd", "success");
}

- (void)didOpenVideoAd
{
    UnitySendMessage([_callbackHandlerName UTF8String], "DidOpenVideoAd", "success");
}

- (void)willCloseVideoAd
{
    UnitySendMessage([_callbackHandlerName UTF8String], "WillCloseVideoAd", "success");
}

- (void)didCloseVideoAd
{
    UnitySendMessage([_callbackHandlerName UTF8String], "DidCloseVideoAd", "success");
}

- (void)showVideoAdFailedWithError:(APError *)error
{
    UnitySendMessage([_callbackHandlerName UTF8String], "ShowVideoAdFailedWithError", [[error description] UTF8String]);
}

// When native code plugin is implemented in .mm / .cpp file, then functions
// should be surrounded with extern "C" block to conform C function naming rules
extern "C" {
	

	void _AdPopcornOfferwallSetCallbackHandler(const char* handlerName)
	{
		[[AdPopcornOfferwallPlugin sharedAdPopcornOfferwallPlugin] setCallbackHandlerName:[NSString stringWithUTF8String:handlerName]];
		NSLog(@"callbackHandlerName: %@", [[AdPopcornOfferwallPlugin sharedAdPopcornOfferwallPlugin] callbackHandlerName]);
	}
    
    void _SetUserDataDictionaryForFilterKeyValue(const char* filterKey, const char* filterValue)
    {
        
        [[AdPopcornOfferwallPlugin sharedAdPopcornOfferwallPlugin] setUserDataDictionaryForFilter:[NSString stringWithUTF8String:filterKey] value:[NSString stringWithUTF8String:filterValue]];
    }
	
    void _OpenOfferWallWithViewController(bool isSetDelegate, bool isUseUserDataDictionaryForFilter)
    {
        id _delegate = nil;
        
        if (isSetDelegate)
        {
            [[AdPopcornOfferwallPlugin sharedAdPopcornOfferwallPlugin] setAdPopcornOfferwallDelegate];
            _delegate = [AdPopcornOfferwall shared].delegate;
        }
        
        NSLog(@"_delegate : %@", _delegate);
        
        if (isUseUserDataDictionaryForFilter)
        {
            // Displays the offer wall.
            [AdPopcornOfferwall openOfferWallWithViewController:UnityGetGLViewController() delegate:_delegate userDataDictionaryForFilter:[[AdPopcornOfferwallPlugin sharedAdPopcornOfferwallPlugin] userDataDictionaryForFilter]];
        }
        else
        {
            [AdPopcornOfferwall openOfferWallWithViewController:UnityGetGLViewController() delegate:_delegate userDataDictionaryForFilter:nil];
        }
    }
    
    void _OpenPromotionEvent(bool isSetDelegate)
    {
        id _delegate = nil;
        
        if (isSetDelegate)
        {
            [[AdPopcornOfferwallPlugin sharedAdPopcornOfferwallPlugin] setAdPopcornOfferwallDelegate];
            _delegate = [AdPopcornOfferwall shared].delegate;
        }
        
        [AdPopcornOfferwall openPromotionEvent:UnityGetGLViewController() delegate:_delegate];
    }
    
    void _LoadVideoAd(bool isSetDelegate)
    {
        id _delegate = nil;
        if(isSetDelegate)
        {
            [[AdPopcornOfferwallPlugin sharedAdPopcornOfferwallPlugin] setAdPopcornOfferwallDelegate];
            _delegate = [AdPopcornOfferwall shared].delegate;
        }
        
        [AdPopcornOfferwall loadVideoAd:_delegate];
    }
    
    void _ShowVideoAdWithViewController(bool isSetDelegate)
    {
        id _delegate = nil;
        if(isSetDelegate)
        {
            [[AdPopcornOfferwallPlugin sharedAdPopcornOfferwallPlugin] setAdPopcornOfferwallDelegate];
            _delegate = [AdPopcornOfferwall shared].delegate;
        }
        [AdPopcornOfferwall showVideoAdWithViewController:UnityGetGLViewController() delegate:_delegate];
    }
    
    void _GetClientPendingRewardItems()
    {
        [AdPopcornOfferwall getClientPendingRewardItems];
    }
    
    void _DidGiveRewardItemWithRewardKey(const char* rewardKey)
    {
        NSLog(@"_DidGiveRewardItemWithRewardKey : rewardKey : %@", [NSString stringWithUTF8String:rewardKey]);
      
        [AdPopcornOfferwall didGiveRewardItemWithRewardKey:[NSString stringWithUTF8String:rewardKey]];
    }
    
    void _SetAdPopcornThemeColor(AdPopcornThemeColor adPopcornThemeColor)
    {
        [AdPopcornStyle sharedInstance].adPopcornThemeColor = adPopcornThemeColor;
    }
    
    void _SetAdPopcornTextThemeColor(AdPopcornThemeColor adPopcornTextThemeColor)
    {
        [AdPopcornStyle sharedInstance].adPopcornTextThemeColor = adPopcornTextThemeColor;
    }
    
    void _SetAdPopcornRewardThemeColor(AdPopcornThemeColor adPopcornRewardThemeColor)
    {
        [AdPopcornStyle sharedInstance].adPopcornRewardThemeColor = adPopcornRewardThemeColor;
    }
    
    void _SetAdPopcornRewardCheckThemeColor(AdPopcornThemeColor adPopcornRewardCheckThemeColor)
    {
        [AdPopcornStyle sharedInstance].adPopcornRewardCheckThemeColor = adPopcornRewardCheckThemeColor;
    }
    
    void _SetAdPopcornOfferWallTitle(const char* adPopcornOfferWallTitle)
    {
        [AdPopcornStyle sharedInstance].adPopcornOfferWallTitle = [NSString stringWithUTF8String:adPopcornOfferWallTitle];
    }
}

@end

