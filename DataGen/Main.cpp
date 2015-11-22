#include <iostream>
#include <stdio.h>
#include <stdlib.h>
#include <time.h>

void main(int args, char* argv[]) {

	double minX = 7.051103f;
	double minY = 43.592690f;
	double maxX = 7.103031f;
	double maxY = 43.628361f;

	double genX, genY;

	srand(time(nullptr));

	for (int i = 0; i < 1100; i++) {
		if (i < 100) {
			genX = (rand() % ((int)((maxX - minX) * 100000))) / 100000.f;
			genX += minX;
			genY = (rand() % ((int)((maxY - minY) * 100000))) / 100000.f;
			genY += minY;
		}
		else if (i < 350) {
			genX = (rand() % ((int)((((maxX - minX) / 2) * 3 / 4) * 100000))) / 100000.f;
			genX += minX + (((maxX - minX) / 2) * 1 / 4) / 2;
			genY = (rand() % ((int)((((maxY - minY) / 2) * 3 / 4) * 100000))) / 100000.f;
			genY += minY + (((maxY - minY) / 2) * 1 / 4) / 2;
		}
		else if (i < 600) {
			genX = (rand() % ((int)((((maxX - minX) / 2) * 3 / 4) * 100000))) / 100000.f;
			genX += minX + (maxX - minX) / 2 + (((maxX - minX) / 2) * 1 / 4) / 2;
			genY = (rand() % ((int)((((maxY - minY) / 2) * 3 / 4) * 100000))) / 100000.f;
			genY += minY + (((maxY - minY) / 2) * 1 / 4) / 2;
		}
		else if (i < 850) {
			genX = (rand() % ((int)((((maxX - minX) / 2) * 3 / 4) * 100000))) / 100000.f;
			genX += minX + (((maxX - minX) / 2) * 1 / 4) / 2;
			genY = (rand() % ((int)((((maxY - minY) / 2) * 3 / 4) * 100000))) / 100000.f;
			genY += minY + (maxY - minY) / 2 + (((maxY - minY) / 2) * 1 / 4) / 2;
		}
		else {
			genX = (rand() % ((int)((((maxX - minX) / 2) * 3 / 4) * 100000))) / 100000.f;
			genX += minX + (maxX - minX) / 2 + (((maxX - minX) / 2) * 1 / 4) / 2;
			genY = (rand() % ((int)((((maxY - minY) / 2) * 3 / 4) * 100000))) / 100000.f;
			genY += minY + (maxY - minY) / 2 + (((maxY - minY) / 2) * 1 / 4) / 2;
		}

		std::cout << genY << "," << genX << std::endl;
	}
}