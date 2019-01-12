import dateutil.parser

#returns 0 if they are the same, -1 if date1 is earlier, 1 if date2 is earlier
def compare(date1, date2):
    if date1.year == date2.year and date1.month == date2.month and date1.day == date2.day:
        if date1.hour == date2.hour and date1.minute == date2.minute and date1.second == date2.second:
            return 0

    if date1.year < date2.year:
        return -1
    elif date1.year > date2.year:
        return 1
    else:
        if date1.month < date2.month:
            return -1
        elif date1.month > date2.month:
            return 1
        else:
            if date1.day < date2.day:
                return -1
            elif date1.day > date2.day:
                return 1
            else:
                if date1.hour < date2.hour:
                    return -1
                elif date1.hour > date2.hour:
                    return 1
                else:
                    if date1.minute < date2.minute:
                        return -1
                    elif date1.minute > date2.minute:
                        return 1
                    else:
                        if date1.second < date2.second:
                            return -1
                        elif date1.second > date2.second:
                            return 1
    pass

def isWithin( testDate, startDate, endDate ):
    if compare(testDate, startDate) == -1 or compare( testDate, endDate) == 1:
        return False
    return True
