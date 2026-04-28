// 1. Function with Required Parameters
function getWelcomeMessage(name: string): string {
  return `Welcome, ${name}! We're glad to have you here.`;
}

// 2. Optional Parameters
function getUserInfo(name: string, age?: number): string {
  if (age !== undefined) {
    return `User: ${name}, Age: ${age}`;
  }
  return `User: ${name} (Age not provided)`;
}

// 3. Default Parameters
function getSubscriptionStatus(name: string, isSubscribed: boolean = false): string {
  if (isSubscribed) {
    return `${name} is currently subscribed to the premium plan.`;
  }
  return `${name} is not subscribed. Upgrade to premium for more benefits!`;
}

// 4. Return Types — function returning boolean
function isEligibleForPremium(age: number): boolean {
  return age > 18;
}

// 5. Arrow Functions — rewriting getWelcomeMessage as arrow function
const getWelcomeMessageArrow = (name: string): string =>
  `Welcome, ${name}! We're glad to have you here.`;

// Arrow version of isEligibleForPremium
const checkPremiumEligibility = (age: number): boolean => age > 18 ? true : false;

// 6. Lexical 'this' 
const NotificationService = {
  appName: "MyAngularApp",

  // Arrow function preserves 'this' from the enclosing context
  sendNotification: function (message: string): void {
    const format = (msg: string): string =>
      `[${this.appName}] Notification: ${msg}`;
    console.log(format(message));
  },

  greetUser: function (name: string): void {
    // Arrow function inside method captures 'this' correctly
    const greet = (): string => `Hello from ${this.appName}, ${name}!`;
    console.log(greet());
  }
};

// 7. Execution — Call all functions and print outputs
console.log("--- Notification System Output ---\n");

// Required parameter
console.log(getWelcomeMessage("Arman"));

// Optional parameter — with and without age
console.log(getUserInfo("rob", 30));
console.log(getUserInfo("skiee"));

// Default parameter — with and without isSubscribed
console.log(getSubscriptionStatus("sheer", true));
console.log(getSubscriptionStatus("kan"));

// Boolean return type
console.log(`Is user aged 20 eligible for premium: ${isEligibleForPremium(20)}`);
console.log(`Is user aged 16 eligible for premium: ${isEligibleForPremium(16)}`);

// Arrow function versions
console.log(getWelcomeMessageArrow("wan"));
console.log(`Arrow premium check for age 22: ${checkPremiumEligibility(22)}`);

// Lexical this in NotificationService
NotificationService.sendNotification("Your account has been updated.");
NotificationService.greetUser("Grace");
