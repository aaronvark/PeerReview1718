import requests
import json
import operator
from timeutil import *
from student import *

##############
# DATA STUFF #
##############

# Modify these dates to change the parsed date range
# date format: YYYY-MM-DDTHH:MM:SSZ, edit to parse other weeks
startDate = "2018-11-12T00:00:00Z"
endDate = "2018-11-19T00:00:00Z"

# OAuth2 Token, run "generate_token.py" to get one
# With a token you can do 5000 github GET requests per hour (without it's 50)
auth_token = ""

# all collaborators, added manually for now (needed push access to list with GET)
studentNames = {
    "aaronvark",
    "vmuijrers",
    "EvocateHimself",
    "DanielBergshoeff",
    "jellebooij",
    "KwakjeBonomel",
    "DatBoiiiii334",
    "WhiteOlivierus",
    "mattyou736",
    "MickGerrit",
    "eMsylf",
    "mcrkersten",
    "AtlasTitanuim",
    "Jerstep",
    "Wesselmast",
    "Timo-Noordzee",
    "Deathjenos",
    "MicksTape",
    "PhuckYuToo",
    "NVriezen",
    "ManOfCheese",
    "Wirantula"
}

sD = dateutil.parser.isoparse(startDate)
eD = dateutil.parser.isoparse(endDate)

# prepare student array for storing class instances
studentList = []

##############
# DATA STUFF # END
##############


# add all collaborators as students
for studentName in studentNames:
    addStudent(studentName, studentList)

# initial get with all pull requests
headers = {"Authorization": "token "+auth_token}
payload = {'state': 'all', 'per_page': '1000'}
r = requests.get("http://api.github.com/repos/aaronvark/PeerReview1718/pulls", headers=headers, params=payload)
j = json.loads(r.text)

numRequests = 1

print(len(j))

# grab each individual request
for i in j:
    # print("Parsing Pull Request: "+str(i["number"]))
    prCreateDate = dateutil.parser.parse(i["created_at"])
    prClosedDate = dateutil.parser.parse(i["created_at"])
    isOpen = (i["state"] == "open")
    if not isOpen:
        print("checking closed date: "+i["closed_at"]+" - "+i["user"]["login"])
        prDate = dateutil.parser.parse(i["closed_at"])

    print(i["state"])

    # WHEN TO SKIP
    # isOpen and createdDate is after endDate (was opened after timeframe)
    # or
    # not isOpen and closedDate is before startDate (already closed during timeframe)
    if not isOpen and compare(prClosedDate, sD) < 0 or isOpen and compare(prCreateDate, eD) > 0:
        print("skipping pull request (not open in window): " + str(i["closed_at"]) + " - " + str(isOpen) + " - " + str(i["user"]["login"]))
        continue

    print("")
    print("*")
    print("")

    # get the name of the owner of the pull request
    owner = i["user"]["login"]

    prOwner = getStudent(owner, studentList)
    print("pull request by: " + owner)
    prOwner.addPR()

    # get reviews for this PR
    headers = {"Authorization": "token " + auth_token}
    r = requests.get(i["url"]+"/reviews", headers=headers)
    reviews = json.loads(r.text)
    numRequests += 1

    reviewers = []

    for re in reviews:
        # print("checking review")
        if len(re["body"]) >= 0:
            # print(re["body"])
            # print(re["author_association"])
            # print(re["state"])
            # if re["author_association"] == "OWNER":
            #    # skip people reviewing their own pull request
            #    continue

            # get name of person that was reviewed
            reviewer = re["user"]["login"]
            print("review by: "+reviewer+ " of "+owner)

            if reviewer == owner:
                # print("person reviewed themselves!")
                continue

            if contains(reviewers, reviewer):
                print("duplicate!")
                continue

            reviewers.append(reviewer)

            print("adding")
            reviewDate = dateutil.parser.parse(re["submitted_at"])
            if isWithin( reviewDate, sD, eD ):
                prReviewer = getStudent(reviewer, studentList)
                prReviewer.addReviewee(owner)
                prReviewer.addReview()
            else:
                print("Skipping review outside of date window")
        pass

    # get review comments for this PR
    url = i["review_comments_url"]
    # use Accept header to also get "likes" etc...
    headers = {"Accept": "application/vnd.github.squirrel-girl-preview", "Authorization": "token "+auth_token}
    r = requests.get(url, headers=headers)
    review_comments = json.loads(r.text)
    numRequests += 1

    for re in review_comments:
        # count all likes, regardless of where a comment lives, but not on people's own comments
        # this should allow for discussions with multiple back and forth likes to be counted

        # parse reactions for each comment
        r = requests.get(re["url"]+"/reactions", headers=headers)
        comment_reactions = json.loads(r.text)
        numRequests += 1
        # print(r.text)
        for reaction in comment_reactions:
            # count +1's, but ignore +1's from the owner of the comment
            if reaction["user"]["id"] == re["user"]["id"]:
                # print("skipping like of own comment for: "+reaction["user"]["login"])
                continue
            if reaction["content"] == "+1":
                print("like from "+reaction["user"]["login"]+" to "+re["user"]["login"])
                likeDate = dateutil.parser.parse(reaction["created_at"])
                if isWithin(likeDate, sD, eD):
                    commenter = re["user"]["login"]
                    liker = reaction["user"]["login"]
                    prCommenter = getStudent(commenter, studentList)
                    prLiker = getStudent(liker, studentList)
                    prLiker.addGivenLike()
                    prCommenter.addLike(int(reaction["id"]))
        pass

for s in studentList:
    s.cacheScore()

studentList.sort(key=operator.attrgetter('scoreValue'))

# print stats
print("***")
print("***",startDate," - ", endDate, "***")
print("***")
print("Score")
for s in studentList:
    print(s.name + ": " + str(s.getScore()))

studentList.sort(key=operator.attrgetter('prs'))

print("")
print("***")
print("Pull Requests made (or: who to ask when I need something to review)")
for s in studentList:
    print(s.name + ": " + str(s.prs))

studentList.sort(key=operator.attrgetter('reviewCount'))

print("")
print("***")
print("Reviews done. Current target: 2")
for s in studentList:
    print(s.name + ": " + str(s.reviewCount))

print("")
print("")
print("num github requests: " + str(numRequests))