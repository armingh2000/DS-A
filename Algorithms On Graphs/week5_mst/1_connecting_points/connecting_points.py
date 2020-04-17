#Uses python3
import sys
import math
import heapq

class element:
    def __init__(self, value, priority):
        self.v = value
        self.p = priority

    def __lt__(self, other):
        return self.p < other.p

def minimum_distance(x, y):
    result = 0.
    #write your code here
    proc_num = 1
    cost = []
    MH = []

    for i in range(len(x)):
        cost.append(float('inf'))
        MH.append(element(i, float('inf')))

    cost[0] = 0
    current = 0
    MH[0].v = 0

    
    while proc_num < len(x):
        #print(dist(MH[0].v, 0, x, y), dist(MH[1].v, 0, x, y))
        current = heapq.heappop(MH).v
        proc_num += 1
        for i in range(len(x)):
            if (i in [m.v for m in MH]) and (cost[i] > dist(current, i, x, y)):
                cost[i] = dist(current, i, x, y)
                findAndReplace(MH, i, cost[i])
        MH.sort()        
                
    
    result = sum(cost)
    return result

def dist(s, t, x, y):
    return math.sqrt((x[s] - x[t]) ** 2 + (y[s] - y[t]) ** 2)

def findAndReplace(h, i, j):
    for k in range(len(h)):
        if h[k].v == i:
            h.pop(k)
            break
    h.append(element(i, j))




if __name__ == '__main__':
##    input = sys.stdin.read()
##    data = list(map(int, input.split()))
##    n = data[0]
##    if n == 0:
##        print(0)
##        exit()
##    x = data[1::2]
##    y = data[2::2]
##    x = [3, 1, 4, 9, 9, 8, 3, 4]
##    y = [1, 2, 6, 8, 9, 9, 11, 12]
    x = []
    y = []
    n = int(input())
    for i in range(n):
        inp = input()
        x.append(int(inp.split()[0]))
        y.append(int(inp.split()[1]))
        
    print("{0:.9f}".format(minimum_distance(x, y)))
