export class User {
  id!: string;
  token!: string;
  username!: string;
  email!: string;
  roles!: string[];

  constructor(id: string, token: string, username: string, email: string, roles: string[]) {
    this.id = id;
    this.token = token;
    this.username = username;
    this.email = email;
    this.roles = roles;
  }
}
