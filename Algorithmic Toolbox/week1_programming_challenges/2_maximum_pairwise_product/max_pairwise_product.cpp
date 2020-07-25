#include <iostream>
#include <vector>
#include <algorithm>

long MaxPairwiseProduct(const std::vector<int>& numbers) {
    int index1 = 0;
    for(int i = 1; i < numbers.size(); i++)
        if(numbers[i] > numbers[index1])
            index1 = i;

    int index2 = index1 == 0 ? 1 : 0;
    for(int i = 0; i < numbers.size(); i++)
        if((i != index1) && (numbers[i] > numbers[index2]))
            index2 = i;

    return (long)numbers[index1] * (long)numbers[index2];
    }

int main() {
    int n;
    std::cin >> n;
    std::vector<int> numbers(n);
    for (int i = 0; i < n; ++i) {
        std::cin >> numbers[i];
    }

    std::cout << MaxPairwiseProduct(numbers) << "\n";
    return 0;
}
