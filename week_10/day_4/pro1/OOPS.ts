class Profile{
    public uname: string;
    public age: number;
    public mail ="lalahmed236@gmail.com";
    public isSubscribed =true;

    constructor( uname: string, age: number ){
        this.uname = uname;
        this.age = age;
    }

    ShowDetails():void{
        this.age++;

        const msg = `Hello ${this.uname} you are ${this.age} years old and your email is ${this.mail}`;
        console.log(msg);

        if(this.age>18 && this.isSubscribed){
            console.log("Eligible for premium plan");
        }
        else{
            console.log("Not eligible for premium plan");
        }
    }
}
    const user = new Profile("Lal",16);
    let TempAge = 25;
        TempAge += 1;

    let city = "Hyderabad";
    let score = 100;
    
    user.ShowDetails();
    console.log("TempAge: ",TempAge);
    console.log("City: ",city, "Score: ",score);
