import requests
import getpass
import json

GITHUB_API = 'https://api.github.com'

def main():
    #
    # User Input
    #
    username = input('Github username: ')
    password = getpass.getpass("Password: ")
    note = input('Note (optional): ')
    #
    # Compose Request
    #
    authUrl = GITHUB_API+'/authorizations'
    payload = {}
    if note:
        payload['note'] = note

    res = requests.post(
        authUrl,
        auth = (username, password),
        data = json.dumps(payload),
        )
    print(res.text)

if __name__ == '__main__':
    main()