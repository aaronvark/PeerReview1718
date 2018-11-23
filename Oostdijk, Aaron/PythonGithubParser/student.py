# stores all data per student
class student:
    def __init__(self, name):
        self.name = name
        self.reviewed = []
        self.likes = []
        self.prs = 0
        self.reviewCount = 0
        self.scoreValue = 0

    def addPR(self):
        if self.prs == 0:
            self.scoreValue += 10
        else:
            self.scoreValue += 1
        self.prs += 1

    def addReview(self):
        if self.reviewCount < 2:
            self.scoreValue += 5
        else:
            self.scoreValue += 1
        self.reviewCount += 1

    def addGivenLike(self):
        self.scoreValue += 1

    def addLike(self, id):
        if self.likes[id] < 1:
            self.scoreValue += 1
        else:
            self.scoreValue += 2
        self.likes[id] += 1

    def addReviewee(self, name):
        for n in self.reviewed:
            # print(n)
            if name == n:
                return
        self.reviewed.append(name)
        # print("Added: "+name)

    def cacheScore(self):
        self.scoreValue = self.getScore()

    def clearScore(self):
        self.scoreValue = 0

    def getScore(self):
        # print(len(self.reviewed))
        # print(self.name + " - likes: "+str(self.likes))
        return self.scoreValue# ( self.likes + self.reviewCount ) * len( self.reviewed )

# functions to make it easier to get students from the list
def getStudent( name, studentList ):
    for s in studentList:
        if s.name == name:
            return s
    return addStudent(name)

def addStudent( name, studentList):
    s = student( name )
    studentList.append( s )
    return s

def contains( list, name ):
    for i in list:
        if i == name:
            return True
    return False